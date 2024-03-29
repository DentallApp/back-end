﻿namespace DentallApp.Core.Appointments.UseCases.GetAvailableHours;

public class GetAvailableHoursValidator : AbstractValidator<AvailableTimeRangeRequest>
{
    public GetAvailableHoursValidator()
    {
        RuleFor(request => request.OfficeId).GreaterThan(0);
        RuleFor(request => request.DentistId).GreaterThan(0);
        RuleFor(request => request.DentalServiceId).GreaterThan(0);
    }
}

public class GetAvailableHoursUseCase(
    IAvailabilityQueries queries, 
    IDateTimeService dateTimeService,
    GetAvailableHoursValidator validator) : IGetAvailableHoursUseCase
{
    public async Task<ListedResult<AvailableTimeRangeResponse>> ExecuteAsync(AvailableTimeRangeRequest request)
    {
        var result = validator.Validate(request);
        if (result.IsFailed())
            return result.Invalid();

        int officeId             = request.OfficeId;
        int dentistId            = request.DentistId;
        int serviceId            = request.DentalServiceId;
        var appointmentDate      = request.AppointmentDate;
        int weekDayId            = (int)appointmentDate.DayOfWeek;
        var weekDayName          = WeekdayCollection.GetName(appointmentDate.DayOfWeek);
        if (await queries.IsPublicHolidayAsync(officeId, appointmentDate.Day, appointmentDate.Month))
            return Result.Failure(Messages.AppointmentDateIsPublicHoliday);

        var employeeSchedule  = await queries.GetEmployeeScheduleAsync(dentistId, weekDayId);
        if (employeeSchedule is null || employeeSchedule.IsEmployeeScheculeDeleted)
            return Result.Failure(new DentistNotAvailableError(weekDayName).Message);

        if (employeeSchedule.IsOfficeScheduleDeleted || employeeSchedule.IsOfficeDeleted)
            return Result.Failure(new OfficeClosedForSpecificDayError(weekDayName).Message);

        if (employeeSchedule.HasNotSchedule())
            return Result.Failure(Messages.NoMorningOrAfternoonHours);

        var unavailables       = await queries.GetUnavailableHoursAsync(dentistId, appointmentDate);
        int? treatmentDuration = await queries.GetTreatmentDurationAsync(serviceId);
        if (treatmentDuration is null)
            return Result.Failure(Messages.DentalServiceNotAvailable);

        TimeSpan serviceDuration = TimeSpan.FromMinutes(treatmentDuration.Value);
        List<AvailableTimeRangeResponse> availableHours = default;

        if (employeeSchedule.HasFullSchedule())
        {
            unavailables.Add(GetTimeOff(employeeSchedule));
            availableHours = Availability.CalculateAvailableHours(new AvailabilityOptions
            {
                DentistStartHour   = (TimeSpan)employeeSchedule.MorningStartHour,
                DentistEndHour     = (TimeSpan)employeeSchedule.AfternoonEndHour,
                ServiceDuration    = serviceDuration,
                AppointmentDate    = appointmentDate,
                CurrentTimeAndDate = dateTimeService.Now,
                Unavailables       = unavailables
                    .OrderBy(timeRange => timeRange.StartHour)
                    .ThenBy(timeRange => timeRange.EndHour)
                    .ToList()
            });
        }
        else if(employeeSchedule.IsMorningSchedule())
        {
            availableHours = Availability.CalculateAvailableHours(new AvailabilityOptions
            {
                DentistStartHour   = (TimeSpan)employeeSchedule.MorningStartHour,
                DentistEndHour     = (TimeSpan)employeeSchedule.MorningEndHour,
                ServiceDuration    = serviceDuration,
                AppointmentDate    = appointmentDate,
                Unavailables       = unavailables,
                CurrentTimeAndDate = dateTimeService.Now
            });
        }
        else if(employeeSchedule.IsAfternoonSchedule())
        {
            availableHours = Availability.CalculateAvailableHours(new AvailabilityOptions
            {
                DentistStartHour   = (TimeSpan)employeeSchedule.AfternoonStartHour,
                DentistEndHour     = (TimeSpan)employeeSchedule.AfternoonEndHour,
                ServiceDuration    = serviceDuration,
                AppointmentDate    = appointmentDate,
                Unavailables       = unavailables,
                CurrentTimeAndDate = dateTimeService.Now
            });
        }

        if(availableHours is null)
            return Result.Failure(Messages.NoSchedulesAvailable);

        return Result.ObtainedResources(availableHours);
    }

    /// <summary>
    /// Gets the employee's time off.
    /// </summary>
    /// <remarks>This time is discarded for the calculation of the available hours.</remarks>
    private static UnavailableTimeRangeResponse GetTimeOff(EmployeeScheduleResponse employeeSchedule) => new()
    {
        StartHour = (TimeSpan)employeeSchedule.MorningEndHour,
        EndHour   = (TimeSpan)employeeSchedule.AfternoonStartHour
    };
}
