namespace DentallApp.Core.PublicHolidays.UseCases;

public class CreatePublicHolidayRequest
{
    public string Description { get; init; }
    public int Day { get; init; }
    public int Month { get; init; }
    public List<int> OfficesId { get; init; }

    public PublicHoliday MapToPublicHoliday() => new()
    {
        Description = Description,
        Day         = Day,
        Month       = Month
    };
}

public class CreatePublicHolidayValidator : AbstractValidator<CreatePublicHolidayRequest>
{
    public CreatePublicHolidayValidator()
    {
        RuleFor(request => request.Description).NotEmpty();
        RuleFor(request => request.Day).InclusiveBetween(1, 31);
        RuleFor(request => request.Month).InclusiveBetween(1, 12);
        RuleFor(request => request.OfficesId).NotEmpty();
        RuleForEach(request => request.OfficesId)
            .ChildRules(validator =>
            {
                validator
                    .RuleFor(officeId => officeId)
                    .GreaterThan(0);
            });
    }
}

public class CreatePublicHolidayUseCase(DbContext context, CreatePublicHolidayValidator validator)
{
    public async Task<Result<CreatedId>> ExecuteAsync(CreatePublicHolidayRequest request)
    {
        var result = validator.Validate(request);
        if (result.IsFailed())
            return result.Invalid();

        var publicHoliday = request.MapToPublicHoliday();
        foreach (int officeId in request.OfficesId.RemoveDuplicates())
        {
            context.Add(new OfficeHoliday { PublicHoliday = publicHoliday, OfficeId = officeId });
        }
        await context.SaveChangesAsync();
        return Result.CreatedResource(publicHoliday.Id);
    }
}
