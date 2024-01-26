namespace DentallApp.Shared.Domain;

/// <summary>
/// Represents a collection containing the days of the week.
/// </summary>
public class WeekdayCollection
{
    private static readonly Dictionary<int, string> _weekDays = new()
    {
        {0, WeekDayName.Sunday },
        {1, WeekDayName.Monday },
        {2, WeekDayName.Tuesday },
        {3, WeekDayName.Wednesday },
        {4, WeekDayName.Thursday },
        {5, WeekDayName.Friday },
        {6, WeekDayName.Saturday }
    };

    /// <summary>
    /// Gets an enumerable to iterate over the days of the week.
    /// </summary>
    public static IEnumerable<KeyValuePair<int, string>> GetAll() 
        => _weekDays;

    /// <summary>
    /// Gets the name of the day of the week in string format.
    /// </summary>
    public static string GetName(DayOfWeek dayOfWeek)
        => _weekDays[(int)dayOfWeek];
}
