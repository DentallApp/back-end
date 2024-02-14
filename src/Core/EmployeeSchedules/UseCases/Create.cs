namespace DentallApp.Core.EmployeeSchedules.UseCases;

public class CreateEmployeeScheduleRequest
{
    public int EmployeeId { get; init; }
    public int WeekDayId { get; init; }
    public TimeSpan? MorningStartHour { get; init; }
    public TimeSpan? MorningEndHour { get; init; }
    public TimeSpan? AfternoonStartHour { get; init; }
    public TimeSpan? AfternoonEndHour { get; init; }

    public EmployeeSchedule MapToEmployeeSchedule() => new()
    {
        EmployeeId         = EmployeeId,
        WeekDayId          = WeekDayId,
        MorningStartHour   = MorningStartHour,
        MorningEndHour     = MorningEndHour,
        AfternoonStartHour = AfternoonStartHour,
        AfternoonEndHour   = AfternoonEndHour
    };
}

public class CreateEmployeeScheduleUseCase(DbContext context)
{
    public async Task<Result<CreatedId>> ExecuteAsync(CreateEmployeeScheduleRequest request)
    {
        var employeeSchedule = request.MapToEmployeeSchedule();
        context.Add(employeeSchedule);
        await context.SaveChangesAsync();
        return Result.CreatedResource(employeeSchedule.Id);
    }
}
