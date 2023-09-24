namespace DentallApp.Shared.Persistence.Repositories;

public interface IOfficeHolidayRepository
{
    /// <summary>
    /// Checks if the day is a public holiday for an office.
    /// </summary>
    Task<bool> IsPublicHolidayAsync(int officeId, int day, int month);
}
