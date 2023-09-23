namespace DentallApp.Domain.Entities;

public class EmployeeSchedule : 
    SoftDeleteEntity, 
    IAuditableEntity, 
    IEmployeeSchedule
{
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; }
    public int WeekDayId { get; set; }
    public WeekDay WeekDay { get; set; }
    public TimeSpan? MorningStartHour { get; set; }
    public TimeSpan? MorningEndHour { get; set; }
    public TimeSpan? AfternoonStartHour { get; set; }
    public TimeSpan? AfternoonEndHour { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
