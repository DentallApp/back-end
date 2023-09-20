namespace DentallApp.Shared.Persistence.Repositories;

public interface IHolidayOfficeRepository : IRepository<HolidayOffice>
{
    /// <summary>
    /// Adds, updates or removes the offices to a public holiday.
    /// </summary>
    /// <param name="publicHolidayId">The ID of the public holiday to update.</param>
    /// <param name="currentHolidayOffices">A collection with the offices assigned to a public holiday.</param>
    /// <param name="officesId">A collection of office identifiers obtained from a web client.</param>
    void UpdateHolidayOffices(int publicHolidayId, List<HolidayOffice> currentHolidayOffices, List<int> officesId);

    Task<bool> IsPublicHolidayAsync(int officeId, int day, int month);
}
