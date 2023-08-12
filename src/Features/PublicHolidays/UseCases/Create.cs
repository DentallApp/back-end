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
}

public static class CreatePublicHolidayMapper
{
    public static PublicHoliday MapToPublicHoliday(this CreatePublicHolidayRequest request)
    {
        return new()
        {
            Description = request.Description,
            Day         = request.Day,
            Month       = request.Month
        };
    }
}

public class CreatePublicHolidayUseCase
{
    private readonly AppDbContext _context;

    public CreatePublicHolidayUseCase(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Response<InsertedIdDto>> Execute(CreatePublicHolidayRequest request)
    {
        var publicHoliday = request.MapToPublicHoliday();
        foreach (int officeId in request.OfficesId.RemoveDuplicates())
        {
            _context.Add(new HolidayOffice { PublicHoliday = publicHoliday, OfficeId = officeId });
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
