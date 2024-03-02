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

public class CreateEmployeeScheduleValidator : AbstractValidator<CreateEmployeeScheduleRequest>
{
    public CreateEmployeeScheduleValidator()
    {
        RuleFor(request => request.EmployeeId).GreaterThan(0);
        RuleFor(request => request.WeekDayId).InclusiveBetween(1, 7);
        RuleFor(request => request.MorningStartHour).NotEmpty();
        RuleFor(request => request.MorningEndHour).NotEmpty();
        RuleFor(request => request.AfternoonStartHour).NotEmpty();
        RuleFor(request => request.AfternoonEndHour).NotEmpty();
    }
}

public class CreateEmployeeScheduleUseCase(DbContext context, CreateEmployeeScheduleValidator validator)
{
    public async Task<Result<CreatedId>> ExecuteAsync(CreateEmployeeScheduleRequest request)
    {
        var result = validator.Validate(request);
        if (result.IsFailed())
            return result.Invalid();

        var employeeSchedule = request.MapToEmployeeSchedule();
        context.Add(employeeSchedule);
        await context.SaveChangesAsync();
        return Result.CreatedResource(employeeSchedule.Id);
    }
}
