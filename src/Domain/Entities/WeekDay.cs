namespace DentallApp.Domain.Entities;

public class WeekDay : EntityBase
{
    public string Name { get; set; }

    /// <summary>
    /// Checks if the collection has consecutive days.
    /// </summary>
    /// <param name="weekDays">
    /// A collection with the days of the week. 
    /// The collection must be sorted in ascending order.
    /// </param>
    /// <returns>
    /// <c>true</c> if the collection has consecutive days; otherwise, <c>false</c>.
    /// </returns>
    private static bool HasConsecutiveDays(List<WeekDay> weekDays)
    {
        for (int i = 1, len = weekDays.Count; i < len; i++)
            if (weekDays[i].Id - weekDays[i - 1].Id != 1)
                return false;

        return true;
    }

    /// <summary>
    /// Converts days of weeks to a range of days.
    /// </summary>
    /// <param name="weekDays">
    /// A collection with the days of the week. 
    /// The collection must be sorted in ascending order.
    /// </param>
    /// <remarks>
    /// Example: Monday to Friday / Monday, Wednesday and Friday.
    /// </remarks>
    public static string ConvertToDayRange(List<WeekDay> weekDays)
    {
        var weekDaysCount = weekDays.Count;
        if (weekDaysCount == 0)
            return string.Empty;

        if (weekDaysCount == 1)
            return weekDays[0].Name;

        if (weekDaysCount == 2)
            return $"{weekDays[0].Name} y {weekDays[1].Name}";

        if (HasConsecutiveDays(weekDays))
            return $"{weekDays[0].Name} a {weekDays[^1].Name}";

        string format = "";
        int i;
        weekDaysCount -= 2;
        for (i = 0; i < weekDaysCount; i++)
            format += weekDays[i].Name + ", ";
        return $"{format}{weekDays[i].Name} y {weekDays[^1].Name}";
    }
}
