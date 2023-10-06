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
    private readonly DbContext _context;
    private readonly IEntityService<OfficeHoliday> _officeHolidayService;

    public UpdatePublicHolidayUseCase(
        DbContext context,
        IEntityService<OfficeHoliday> officeHolidayService)
    {
        _context = context;
        _officeHolidayService = officeHolidayService;
    }

    public async Task<Response> ExecuteAsync(int id, UpdatePublicHolidayRequest request)
    {
        var holiday = await _context.Set<PublicHoliday>()
            .Include(publicHoliday => publicHoliday.Offices)
            .Where(publicHoliday => publicHoliday.Id == id)
            .FirstOrDefaultAsync();

        if (holiday is null)
            return new Response(ResourceNotFoundMessage);

        request.MapToPublicHoliday(holiday);
        AssignOfficesToHoliday(holiday.Id, holiday.Offices, request.OfficesId);
        await _context.SaveChangesAsync();

        return new Response
        {
            Success = true,
            Message = UpdateResourceMessage
        };
    }

    /// <summary>
    /// Assigns offices to a public holiday.
    /// </summary>
    /// <param name="publicHolidayId">The ID of the public holiday to assign.</param>
    /// <param name="currentOffices">A collection with the offices assigned to a public holiday.</param>
    /// <param name="officesId">A collection of office identifiers obtained from a client.</param>
    private void AssignOfficesToHoliday(
        int publicHolidayId, 
        List<OfficeHoliday> currentOffices, 
        List<int> officesId)
    {
        _officeHolidayService.Update(publicHolidayId, ref currentOffices, ref officesId);
    }
}
