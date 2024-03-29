﻿namespace DentallApp.Core.Appointments.UseCases.GetAvailableHours;

/// <summary>
/// Contains helper queries that are used in the <see cref="GetAvailableHoursUseCase"/>.
/// </summary>
public interface IAvailabilityQueries
{
    Task<List<UnavailableTimeRangeResponse>> GetUnavailableHoursAsync(int dentistId, DateTime appointmentDate);
    Task<EmployeeScheduleResponse> GetEmployeeScheduleAsync(int employeeId, int weekDayId);
    Task<int?> GetTreatmentDurationAsync(int generalTreatmentId);

    /// <summary>
    /// Checks if the day is a public holiday for an office.
    /// </summary>
    Task<bool> IsPublicHolidayAsync(int officeId, int day, int month);
}

/// <inheritdoc cref="IAvailabilityQueries"/>
public class AvailabilityQueries(DbContext context, IDateTimeService dateTimeService) : IAvailabilityQueries
{
    public async Task<EmployeeScheduleResponse> GetEmployeeScheduleAsync(int employeeId, int weekDayId)
    {
        var schedules = await 
            (from employeeSchedule in context.Set<EmployeeSchedule>()
                join employee in context.Set<Employee>() on employeeSchedule.EmployeeId equals employee.Id
                join office in context.Set<Office>() on employee.OfficeId equals office.Id
                join officeSchedule in context.Set<OfficeSchedule>() on
                new
                {
                    WeekDayId = weekDayId,
                    office.Id
                }
                equals
                new
                {
                    officeSchedule.WeekDayId,
                    Id = officeSchedule.OfficeId
                }
                where employee.Id == employeeId && employeeSchedule.WeekDayId == weekDayId
                select new EmployeeScheduleResponse
                {
                    EmployeeScheduleId        = employeeSchedule.Id,
                    MorningStartHour          = employeeSchedule.MorningStartHour,
                    MorningEndHour            = employeeSchedule.MorningEndHour,
                    AfternoonStartHour        = employeeSchedule.AfternoonStartHour,
                    AfternoonEndHour          = employeeSchedule.AfternoonEndHour,
                    IsEmployeeScheculeDeleted = employeeSchedule.IsDeleted,
                    OfficeId                  = employee.OfficeId,
                    IsOfficeDeleted           = office.IsDeleted,
                    IsOfficeScheduleDeleted   = officeSchedule.IsDeleted
                })
                .IgnoreQueryFilters()
                .AsNoTracking()
                .FirstOrDefaultAsync();

        return schedules;
    }

    public async Task<int?> GetTreatmentDurationAsync(int generalTreatmentId)
    {
        var treatment = await context.Set<GeneralTreatment>()
            .Where(treatment => treatment.Id == generalTreatmentId)
            .Select(treatment => new 
            { 
                treatment.Duration 
            })
            .IgnoreQueryFilters()
            .AsNoTracking()
            .FirstOrDefaultAsync();

        return treatment?.Duration;
    }

    public async Task<List<UnavailableTimeRangeResponse>> GetUnavailableHoursAsync(int dentistId, DateTime appointmentDate)
    {
        var unavailableHours = await context.Set<Appointment>()
            .Where(appointment =>
                  (appointment.DentistId == dentistId) &&
                  (appointment.Date == appointmentDate) &&
                  (appointment.IsNotCanceled() ||
                   appointment.IsCancelledByEmployee ||
                   // Checks if the canceled appointment is not available.
                   // This check allows patients to choose a time slot for an appointment canceled by another basic user.
                   dateTimeService.Now > DBFunctions.AddTime(DBFunctions.ToDateTime(appointment.Date), appointment.StartHour)))
            .Select(appointment => new UnavailableTimeRangeResponse
            {
                StartHour = appointment.StartHour,
                EndHour   = appointment.EndHour
            })
            .Distinct()
            .OrderBy(appointment => appointment.StartHour)
              .ThenBy(appointment => appointment.EndHour)
            .AsNoTracking()
            .ToListAsync();

        return unavailableHours;
    }

    public async Task<bool> IsPublicHolidayAsync(int officeId, int day, int month)
    {
        var query = (from officeHoliday in context.Set<OfficeHoliday>()
            join publicHoliday in context.Set<PublicHoliday>() on officeHoliday.PublicHolidayId equals publicHoliday.Id
            where officeHoliday.OfficeId == officeId &&
                  publicHoliday.Day == day &&
                  publicHoliday.Month == month
            select officeHoliday.Id);

        return await query.FirstOrDefaultAsync() > 0;
    }
}
