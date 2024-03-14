namespace DentallApp.Shared.Entities.EmployeeSchedules;

public static class IEmployeeScheduleExtensions
{
    /// <summary>
    /// Checks if the employee's schedule is in the morning.
    /// </summary>
    public static bool IsMorningSchedule(this IEmployeeSchedule employeeSchedule)
    {
        return employeeSchedule.MorningStartHour is not null &&
               employeeSchedule.MorningEndHour is not null;
    }

    /// <summary>
    /// Checks if the employee's schedule is in the afternoon.
    /// </summary>
    public static bool IsAfternoonSchedule(this IEmployeeSchedule employeeSchedule)
    {
        return employeeSchedule.AfternoonStartHour is not null &&
               employeeSchedule.AfternoonEndHour is not null;
    }

    /// <summary>
    /// Checks if the employee does not have morning and afternoon schedule.
    /// </summary>
    public static bool HasNotSchedule(this IEmployeeSchedule employeeSchedule)
    {
        return !employeeSchedule.IsMorningSchedule() &&
               !employeeSchedule.IsAfternoonSchedule();
    }

    /// <summary>
    /// Checks if the employee has a morning and afternoon schedule.
    /// </summary>
    public static bool HasFullSchedule(this IEmployeeSchedule employeeSchedule)
    {
        return employeeSchedule.IsMorningSchedule() &&
               employeeSchedule.IsAfternoonSchedule();
    }
}
