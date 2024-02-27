namespace DentallApp.Core.OfficeSchedules.UseCases;

public class UpdateOfficeScheduleRequest
{
    public int WeekDayId { get; init; }
    public TimeSpan StartHour { get; init; }
    public TimeSpan EndHour { get; init; }
    public bool IsDeleted { get; init; }

    public void MapToOfficeSchedule(OfficeSchedule schedule)
    {
        schedule.WeekDayId = WeekDayId;
        schedule.StartHour = StartHour;
        schedule.EndHour   = EndHour;
        schedule.IsDeleted = IsDeleted;
    }
}

public class UpdateOfficeScheduleValidator : AbstractValidator<UpdateOfficeScheduleRequest>
{
    public UpdateOfficeScheduleValidator()
    {
        RuleFor(request => request.WeekDayId).InclusiveBetween(1, 7);
        RuleFor(request => request.StartHour).LessThan(request => request.EndHour);
    }
}

public class UpdateOfficeScheduleUseCase(DbContext context, UpdateOfficeScheduleValidator validator)
{
    public async Task<Result> ExecuteAsync(int id, ClaimsPrincipal currentEmployee, UpdateOfficeScheduleRequest request)
    {
        var result = validator.Validate(request);
        if (result.IsFailed())
            return result.Invalid();

        var officeSchedule = await context.Set<OfficeSchedule>()
            .Where(officeSchedule => officeSchedule.Id == id)
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync();

        if (officeSchedule is null)
            return Result.NotFound();

        if (currentEmployee.IsAdmin() && currentEmployee.IsNotInOffice(officeSchedule.OfficeId))
            return Result.Forbidden(Messages.OfficeNotAssigned);

        request.MapToOfficeSchedule(officeSchedule);
        await context.SaveChangesAsync();
        return Result.UpdatedResource();
    }
}
