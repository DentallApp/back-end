namespace DentallApp.Features.EmployeeSchedules;

public static class EmployeeScheduleExtensions
{
    /// <summary>
    /// Comprueba sí el horario del empleado es de mañana.
    /// </summary>
    public static bool IsMorningSchedule(this IEmployeeScheduleDto employeeSchedule)
        => employeeSchedule.MorningStartHour is not null && employeeSchedule.MorningEndHour is not null;

    /// <summary>
    /// Comprueba sí el horario del empleado es de tarde.
    /// </summary>
    public static bool IsAfternoonSchedule(this IEmployeeScheduleDto employeeSchedule)
        => employeeSchedule.AfternoonStartHour is not null && employeeSchedule.AfternoonEndHour is not null;

    /// <summary>
    /// Comprueba sí el empleado no tiene horario de mañana ni de tarde.
    /// </summary>
    public static bool HasNotSchedule(this IEmployeeScheduleDto employeeSchedule)
        => !employeeSchedule.IsMorningSchedule() && !employeeSchedule.IsAfternoonSchedule();

    /// <summary>
    /// Comprueba sí el empleado tiene un horario de mañana y tarde.
    /// </summary>
    public static bool HasFullSchedule(this IEmployeeScheduleDto employeeSchedule)
        => employeeSchedule.IsMorningSchedule() && employeeSchedule.IsAfternoonSchedule();
}
