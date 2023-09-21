namespace DentallApp.Infrastructure.Persistence.Repositories;

public class OfficeHolidayRepository : IOfficeHolidayRepository
{
    private readonly AppDbContext _context;

    public OfficeHolidayRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> IsPublicHoliday(int officeId, int day, int month)
    {
        var query = (from holidayOffice in _context.Set<HolidayOffice>()
                     join publicHoliday in _context.Set<PublicHoliday>() on holidayOffice.PublicHolidayId equals publicHoliday.Id
                     where holidayOffice.OfficeId == officeId &&
                           publicHoliday.Day == day &&
                           publicHoliday.Month == month
                     select holidayOffice.Id);

        return await query.FirstOrDefaultAsync() > 0;
    }
}
