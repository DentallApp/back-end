namespace DentallApp.Features.PublicHolidays;

public class PublicHolidayRepository : Repository<PublicHoliday>, IPublicHolidayRepository
{
    public PublicHolidayRepository(AppDbContext context) : base(context) { }

    public async Task<PublicHoliday> GetPublicHolidayAsync(int id)
        => await Context.Set<PublicHoliday>()
                        .Include(publicHoliday => publicHoliday.HolidayOffices)
                        .Where(publicHoliday => publicHoliday.Id == id)
                        .FirstOrDefaultAsync();

    public async Task<IEnumerable<PublicHolidayGetAllDto>> GetPublicHolidaysAsync()
        => await Context.Set<PublicHoliday>()
                        .Include(publicHoliday => publicHoliday.HolidayOffices)
                           .ThenInclude(holidayOffice => holidayOffice.Office)
                        .Select(publicHoliday => publicHoliday.MapToPublicHolidayGetAllDto())
                        .ToListAsync();
}
