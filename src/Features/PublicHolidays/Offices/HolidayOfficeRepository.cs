namespace DentallApp.Features.PublicHolidays.Offices;

public class HolidayOfficeRepository : Repository<HolidayOffice>, IHolidayOfficeRepository
{
    public HolidayOfficeRepository(AppDbContext context) : base(context) { }

    /// <inheritdoc />
    public void UpdateHolidayOffices(int publicHolidayId, List<HolidayOffice> currentHolidayOffices, List<int> officesId)
        => this.AddOrUpdateOrDelete(key: publicHolidayId, source: ref currentHolidayOffices, identifiers: ref officesId);
}
