namespace DentallApp.Infrastructure.Persistence.Repositories;

public class HolidayOfficeRepository : Repository<HolidayOffice>, IHolidayOfficeRepository
{
    public HolidayOfficeRepository(AppDbContext context) : base(context) { }

    /// <inheritdoc />
    public void UpdateHolidayOffices(int publicHolidayId, List<HolidayOffice> currentHolidayOffices, List<int> officesId)
        => this.AddOrUpdateOrDelete(key: publicHolidayId, source: ref currentHolidayOffices, identifiers: ref officesId);

    public async Task<bool> IsPublicHolidayAsync(int officeId, int day, int month)
    {
        var query = (from holidayOffice in Context.Set<HolidayOffice>()
                     join publicHoliday in Context.Set<PublicHoliday>() on holidayOffice.PublicHolidayId equals publicHoliday.Id
                     where holidayOffice.OfficeId == officeId &&
                           publicHoliday.Day == day &&
                           publicHoliday.Month == month
                     select holidayOffice.Id);
        return await query.FirstOrDefaultAsync() > 0;
    }


}
