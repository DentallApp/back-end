namespace DentallApp.Features.Offices;

public static class OfficeScheduleList
{
    /// <summary>
    /// Adds the missing schedules in the current collection.
    /// </summary>
    /// <param name="schedules">The current collection with office schedules.</param>
    /// <param name="message">An optional message to indicate that the schedule is not available.</param>
    /// <returns>
    /// A collection with the complete schedules, including missing schedules.
    /// </returns>
    /// <remarks>
    /// If all the schedules are complete, then the current collection is returned.
    /// </remarks>
    public static List<OfficeScheduleResponse> AddMissingSchedules(
        this List<OfficeScheduleResponse> schedules,
        string message = NotAvailableMessage)
    {
        if (schedules.Count == WeekDaysType.MaxWeekDay)
            return schedules;

        foreach (var (weekDayId, weekDayName) in WeekDaysType.WeekDays)
        {
            if (schedules.Find(schedule => schedule.WeekDayId == weekDayId) is null)
            {
                schedules.Add(new OfficeScheduleResponse
                {
                    WeekDayId   = weekDayId,
                    WeekDayName = weekDayName,
                    Schedule    = message
                });
            }
        }
        return schedules.OrderBy(schedule => schedule.WeekDayId).ToList();
    }
}
