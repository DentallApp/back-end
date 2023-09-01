namespace DentallApp.Features.OfficeSchedules.UseCases;

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

public class GetSchedulesByOfficeIdUseCase
{
    private readonly AppDbContext _context;

    public GetSchedulesByOfficeIdUseCase(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<GetSchedulesByOfficeIdResponse>> Execute(int officeId)
    {
        var schedules = await _context.Set<OfficeSchedule>()
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
