namespace UnitTests.Shared.Entities.EmployeeSchedules;

public class EmployeeScheduleFake : IEmployeeSchedule
{
    public TimeSpan? MorningStartHour { get; set; }
    public TimeSpan? MorningEndHour { get; set; }
    public TimeSpan? AfternoonStartHour { get; set; }
    public TimeSpan? AfternoonEndHour { get; set; }
}
