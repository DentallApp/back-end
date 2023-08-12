namespace DentallApp.Features.PublicHolidays.UseCases;

public class GetPublicHolidaysResponse
{
    public class Office
    {
        public int Id { get; init; }
        public string Name { get; init; }
    }

    public int Id { get; init; }
    public string Description { get; init; }
    public int Day { get; init; }
    public int Month { get; init; }
    public IEnumerable<Office> Offices { get; init; }
}

public class GetPublicHolidaysUseCase
{
    private readonly AppDbContext _context;

    public GetPublicHolidaysUseCase(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<GetPublicHolidaysResponse>> Execute()
    {
        var holidays = await _context.Set<PublicHoliday>()
            .Select(publicHoliday => new GetPublicHolidaysResponse
            {
                Id          = publicHoliday.Id,
                Description = publicHoliday.Description,
                Day         = publicHoliday.Day,
                Month       = publicHoliday.Month,
                Offices     = publicHoliday.HolidayOffices
                   .Select(holidayOffice => new GetPublicHolidaysResponse.Office
                   {
                        Id   = holidayOffice.OfficeId,
                        Name = holidayOffice.Office.Name
                   })
            })
            .ToListAsync();

        return holidays;
    }
}
