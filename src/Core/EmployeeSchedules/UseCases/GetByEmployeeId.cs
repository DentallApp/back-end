namespace DentallApp.Core.EmployeeSchedules.UseCases;

public class GetSchedulesByEmployeeIdResponse
{
    public int ScheduleId { get; init; }
    public int WeekDayId { get; init; }
    public string WeekDayName { get; init; }
    public string Status { get; init; }
    public bool IsDeleted { get; init; }
    public string MorningStartHour { get; init; }
    public string MorningEndHour { get; init; }
    public string AfternoonStartHour { get; init; }
    public string AfternoonEndHour { get; init; }
}

public class GetSchedulesByEmployeeIdUseCase(DbContext context)
{
    public async Task<IEnumerable<GetSchedulesByEmployeeIdResponse>> ExecuteAsync(int employeeId)
    {
        var schedules = await context.Set<EmployeeSchedule>()
            .Where(employeeSchedule => employeeSchedule.EmployeeId == employeeId)
            .OrderBy(employeeSchedule => employeeSchedule.WeekDayId)
            .Select(employeeSchedule => new GetSchedulesByEmployeeIdResponse
            {
                ScheduleId         = employeeSchedule.Id,
                WeekDayId          = employeeSchedule.WeekDayId,
                WeekDayName        = employeeSchedule.WeekDay.Name,
                Status             = employeeSchedule.GetStatusName(),
                IsDeleted          = employeeSchedule.IsDeleted,
                MorningStartHour   = employeeSchedule.MorningStartHour.GetHourWithoutSeconds(),
                MorningEndHour     = employeeSchedule.MorningEndHour.GetHourWithoutSeconds(),
                AfternoonStartHour = employeeSchedule.AfternoonStartHour.GetHourWithoutSeconds(),
                AfternoonEndHour   = employeeSchedule.AfternoonEndHour.GetHourWithoutSeconds()
            })
            .IgnoreQueryFilters()
            .AsNoTracking()
            .ToListAsync();

        return schedules;
    }
}
