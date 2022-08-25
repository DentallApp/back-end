namespace DentallApp.Features.EmployeeSchedules;

public class EmployeeSchedule : ModelWithSoftDelete
{
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; }
    public int WeekDayId { get; set; }
    public WeekDay WeekDay { get; set; }
    public int OfficeId { get; set; }
    public Office Office { get; set; }
    public TimeSpan? MorningStartHour { get; set; }
    public TimeSpan? MorningEndHour { get; set; }
    public TimeSpan? AfternoonStartHour { get; set; }
    public TimeSpan? AfternoonEndHour { get; set; }
}
