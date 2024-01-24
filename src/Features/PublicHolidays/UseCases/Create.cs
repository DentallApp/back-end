namespace DentallApp.Features.PublicHolidays.UseCases;

public class CreatePublicHolidayRequest
{
    [Required]
    public string Description { get; init; }
    [Range(1, 31)]
    public int Day { get; init; }
    [Range(1, 12)]
    public int Month { get; init; }
    [Required]
    public List<int> OfficesId { get; init; }

    public PublicHoliday MapToPublicHoliday() => new()
    {
        Description = Description,
        Day         = Day,
        Month       = Month
    };
}

public class CreatePublicHolidayUseCase(DbContext context)
{
    public async Task<Result<CreatedId>> ExecuteAsync(CreatePublicHolidayRequest request)
    {
        var publicHoliday = request.MapToPublicHoliday();
        foreach (int officeId in request.OfficesId.RemoveDuplicates())
        {
            context.Add(new OfficeHoliday { PublicHoliday = publicHoliday, OfficeId = officeId });
        }
        await context.SaveChangesAsync();
        return Result.CreatedResource(publicHoliday.Id);
    }
}
