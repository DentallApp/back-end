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

    public PublicHoliday MapToPublicHoliday()
    {
        return new()
        {
            Description = Description,
            Day         = Day,
            Month       = Month
        };
    }
}

public class CreatePublicHolidayUseCase
{
    private readonly DbContext _context;

    public CreatePublicHolidayUseCase(DbContext context)
    {
        _context = context;
    }

    public async Task<Response<InsertedIdDto>> ExecuteAsync(CreatePublicHolidayRequest request)
    {
        var publicHoliday = request.MapToPublicHoliday();
        foreach (int officeId in request.OfficesId.RemoveDuplicates())
        {
            _context.Add(new OfficeHoliday { PublicHoliday = publicHoliday, OfficeId = officeId });
        }
        await _context.SaveChangesAsync();

        return new Response<InsertedIdDto>
        {
            Data    = new InsertedIdDto { Id = publicHoliday.Id },
            Success = true,
            Message = CreateResourceMessage
        };
    }
}
