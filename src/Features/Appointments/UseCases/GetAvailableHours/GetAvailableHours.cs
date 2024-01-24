namespace DentallApp.Features.Appointments.UseCases.GetAvailableHours;

public class GetAvailableHoursUseCase(IAvailabilityQueries queries, IDateTimeService dateTimeService) : IGetAvailableHoursUseCase
{
    public async Task<ListedResult<AvailableTimeRangeResponse>> ExecuteAsync(AvailableTimeRangeRequest request)
    {
        int officeId             = request.OfficeId;
        int dentistId            = request.DentistId;
        int serviceId            = request.DentalServiceId;
        var appointmentDate      = request.AppointmentDate;
        int weekDayId            = (int)appointmentDate.DayOfWeek;
        var weekDayName          = WeekDaysType.WeekDays[weekDayId];
        if (await queries.IsPublicHolidayAsync(officeId, appointmentDate.Day, appointmentDate.Month))
            return Result.Failure(AppointmentDateIsPublicHolidayMessage);

        var employeeSchedule  = await queries.GetEmployeeScheduleAsync(dentistId, weekDayId);
        if (employeeSchedule is null || employeeSchedule.IsEmployeeScheculeDeleted)
            return Result.Failure(string.Format(DentistNotAvailableMessage, weekDayName));

        if (employeeSchedule.IsOfficeScheduleDeleted || employeeSchedule.IsOfficeDeleted)
            return Result.Failure(string.Format(OfficeClosedForSpecificDayMessage, weekDayName));

        if (employeeSchedule.HasNotSchedule())
            return Result.Failure(NoMorningOrAfternoonHoursMessage);

        var unavailables       = await queries.GetUnavailableHoursAsync(dentistId, appointmentDate);
        int? treatmentDuration = await queries.GetTreatmentDurationAsync(serviceId);
        if (treatmentDuration is null)
            return Result.Failure(DentalServiceNotAvailableMessage);

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
            return Result.Failure(NoSchedulesAvailableMessage);

        return Result.ObtainedResources(availableHours);
    }

    /// <summary>
    /// Obtiene el tiempo libre (o punto de descanso) del empleado. 
    /// Este tiempo se descarta para el cálculo de los horarios disponibles.
    /// </summary>
    private static UnavailableTimeRangeResponse GetTimeOff(EmployeeScheduleResponse employeeSchedule) => new()
    {
        StartHour = (TimeSpan)employeeSchedule.MorningEndHour,
        EndHour   = (TimeSpan)employeeSchedule.AfternoonStartHour
    };
}
