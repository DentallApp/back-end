namespace DentallApp.Infrastructure.Persistence.Repositories;

public class OfficeHolidayRepository : IOfficeHolidayRepository
{
    private readonly DbContext _context;

    public OfficeHolidayRepository(DbContext context)
    {
        _context = context;
    }

    public async Task<bool> IsPublicHolidayAsync(int officeId, int day, int month)
    {
        var query = (from officeHoliday in _context.Set<OfficeHoliday>()
            join publicHoliday in _context.Set<PublicHoliday>() on officeHoliday.PublicHolidayId equals publicHoliday.Id
            where officeHoliday.OfficeId == officeId &&
                  publicHoliday.Day == day &&
                  publicHoliday.Month == month
            select officeHoliday.Id);

        return await query.FirstOrDefaultAsync() > 0;
    }
}
