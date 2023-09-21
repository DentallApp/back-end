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
    private readonly IEntityService<HolidayOffice> _holidayOfficeService;

    public UpdatePublicHolidayUseCase(
        AppDbContext context,
        IEntityService<HolidayOffice> holidayOfficeService)
    {
        _context = context;
        _holidayOfficeService = holidayOfficeService;
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
        UpdateHolidayOffices(holiday.Id, holiday.HolidayOffices, request.OfficesId);
        await _context.SaveChangesAsync();

        return new Response
        {
            Success = true,
            Message = UpdateResourceMessage
        };
    }

    /// <summary>
    /// Updates the offices to a public holiday.
    /// </summary>
    /// <param name="publicHolidayId">The ID of the public holiday to update.</param>
    /// <param name="currentHolidayOffices">A collection with the offices assigned to a public holiday.</param>
    /// <param name="officesId">A collection of office identifiers obtained from a client.</param>
    private void UpdateHolidayOffices(
        int publicHolidayId, 
        List<HolidayOffice> currentHolidayOffices, 
        List<int> officesId)
    {
        _holidayOfficeService.Update(publicHolidayId, ref currentHolidayOffices, ref officesId);
    }
}
