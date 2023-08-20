namespace DentallApp.Features.PublicHolidays.UseCases;

public class UpdatePublicHolidayRequest
{
    [Required]
    public string Description { get; init; }
    [Range(1, 31)]
    public int Day { get; init; }
    [Range(1, 12)]
    public int Month { get; init; }
    [Required]
    public List<int> OfficesId { get; init; }

    public void MapToPublicHoliday(PublicHoliday publicHoliday)
    {
        publicHoliday.Description = Description;
        publicHoliday.Day         = Day;
        publicHoliday.Month       = Month;
    }
}

public class UpdatePublicHolidayUseCase
{
    private readonly AppDbContext _context;
    private readonly IHolidayOfficeRepository _holidayRepository;

    public UpdatePublicHolidayUseCase(AppDbContext context, IHolidayOfficeRepository holidayRepository)
    {
        _context = context;
        _holidayRepository = holidayRepository;
    }

    public async Task<Response> Execute(int id, UpdatePublicHolidayRequest request)
    {
        var holiday = await _context.Set<PublicHoliday>()
            .Include(publicHoliday => publicHoliday.HolidayOffices)
            .Where(publicHoliday => publicHoliday.Id == id)
            .FirstOrDefaultAsync();

        if (holiday is null)
            return new Response(ResourceNotFoundMessage);

        request.MapToPublicHoliday(holiday);
        _holidayRepository.UpdateHolidayOffices(holiday.Id, holiday.HolidayOffices, request.OfficesId);
        await _context.SaveChangesAsync();

        return new Response
        {
            Success = true,
            Message = UpdateResourceMessage
        };
    }
}
