namespace DentallApp.Entities;

public class OfficeSchedule : SoftDeleteEntity
{
    public int WeekDayId { get; set; }
    public WeekDay WeekDay { get; set; }
    public int OfficeId { get; set; }
    public Office Office { get; set; }
    public TimeSpan StartHour { get; set; }
    public TimeSpan EndHour { get; set; }
}
