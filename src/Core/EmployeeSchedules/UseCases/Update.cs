namespace DentallApp.Core.EmployeeSchedules.UseCases;

public class UpdateEmployeeScheduleRequest
{
    public int WeekDayId { get; init; }
    public bool IsDeleted { get; init; }
    public TimeSpan? MorningStartHour { get; init; }
    public TimeSpan? MorningEndHour { get; init; }
    public TimeSpan? AfternoonStartHour { get; init; }
    public TimeSpan? AfternoonEndHour { get; init; }

    public void MapToEmployeeSchedule(EmployeeSchedule schedule)
    {
        schedule.WeekDayId          = WeekDayId;
        schedule.IsDeleted          = IsDeleted;
        schedule.MorningStartHour   = MorningStartHour;
        schedule.MorningEndHour     = MorningEndHour;
        schedule.AfternoonStartHour = AfternoonStartHour;
        schedule.AfternoonEndHour   = AfternoonEndHour;
    }
}

public class UpdateEmployeeScheduleUseCase(DbContext context)
{
    public async Task<Result> ExecuteAsync(int id, UpdateEmployeeScheduleRequest request)
    {
        var employeeSchedule = await context.Set<EmployeeSchedule>()
            .Where(employeeSchedule => employeeSchedule.Id == id)
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync();

        if (employeeSchedule is null)
            return Result.NotFound();

        request.MapToEmployeeSchedule(employeeSchedule);
        await context.SaveChangesAsync();
        return Result.UpdatedResource();
    }
}
