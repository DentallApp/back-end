namespace DentallApp.Core.OfficeSchedules.UseCases;

public class GetSchedulesByOfficeIdResponse
{
    public int ScheduleId { get; init; }
    public int WeekDayId { get; init; }
    public string WeekDayName { get; init; }
    public string Status { get; init; }
    public bool IsDeleted { get; init; }
    public string StartHour { get; init; }
    public string EndHour { get; init; }
}

public class GetSchedulesByOfficeIdUseCase(DbContext context)
{
    public async Task<IEnumerable<GetSchedulesByOfficeIdResponse>> ExecuteAsync(int officeId)
    {
        var schedules = await context.Set<OfficeSchedule>()
            .Where(officeSchedule => officeSchedule.OfficeId == officeId)
            .OrderBy(officeSchedule => officeSchedule.WeekDayId)
            .Select(officeSchedule => new GetSchedulesByOfficeIdResponse
            {
                ScheduleId  = officeSchedule.Id,
                WeekDayId   = officeSchedule.WeekDayId,
                WeekDayName = officeSchedule.WeekDay.Name,
                Status      = officeSchedule.GetStatusName(),
                IsDeleted   = officeSchedule.IsDeleted,
                StartHour   = officeSchedule.StartHour.GetHourWithoutSeconds(),
                EndHour     = officeSchedule.EndHour.GetHourWithoutSeconds()
            })
            .IgnoreQueryFilters()
            .AsNoTracking()
            .ToListAsync();

        return schedules;
    }
}
