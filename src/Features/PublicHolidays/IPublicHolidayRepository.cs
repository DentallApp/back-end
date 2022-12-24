namespace DentallApp.Features.PublicHolidays;

public interface IPublicHolidayRepository : IRepository<PublicHoliday>
{
    Task<PublicHoliday> GetPublicHolidayAsync(int id);
    Task<IEnumerable<PublicHolidayGetAllDto>> GetPublicHolidaysAsync();
}
