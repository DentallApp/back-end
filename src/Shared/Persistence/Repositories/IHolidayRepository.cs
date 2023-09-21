namespace DentallApp.Shared.Persistence.Repositories;

public interface IHolidayRepository
{
    /// <summary>
    /// Checks if the day is a public holiday for an office.
    /// </summary>
    Task<bool> IsPublicHoliday(int officeId, int day, int month);
}
