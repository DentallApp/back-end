namespace DentallApp.Core.OfficeSchedules.UseCases;

public class CreateOfficeScheduleRequest
{
    public int WeekDayId { get; init; }
    public int OfficeId { get; init; }
    public TimeSpan StartHour { get; init; }
    public TimeSpan EndHour { get; init; }

    public OfficeSchedule MapToOfficeSchedule() => new()
    {
        WeekDayId = WeekDayId,
        OfficeId  = OfficeId,
        StartHour = StartHour,
        EndHour   = EndHour
    };
}

public class CreateOfficeScheduleValidator : AbstractValidator<CreateOfficeScheduleRequest>
{
    public CreateOfficeScheduleValidator()
    {
        RuleFor(request => request.WeekDayId).InclusiveBetween(1, 7);
        RuleFor(request => request.OfficeId).GreaterThan(0);
        RuleFor(request => request.StartHour).LessThan(request => request.EndHour);
    }
}

public class CreateOfficeScheduleUseCase(DbContext context, CreateOfficeScheduleValidator validator)
{
    public async Task<Result<CreatedId>> ExecuteAsync(ClaimsPrincipal currentEmployee, CreateOfficeScheduleRequest request)
    {
        var result = validator.Validate(request);
        if (result.IsFailed())
            return result.Invalid();

        if (currentEmployee.IsAdmin() && currentEmployee.IsNotInOffice(request.OfficeId))
            return Result.Forbidden(Messages.OfficeNotAssigned);

        var officeSchedule = request.MapToOfficeSchedule();
        context.Add(officeSchedule);
        await context.SaveChangesAsync();
        return Result.CreatedResource(officeSchedule.Id);
    }
}
