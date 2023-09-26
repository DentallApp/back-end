namespace DentallApp.Domain;

public class OfficeSchedule : SoftDeleteEntity, IAuditableEntity
{
    public int WeekDayId { get; set; }
    public WeekDay WeekDay { get; set; }
    public int OfficeId { get; set; }
    public Office Office { get; set; }
    public TimeSpan StartHour { get; set; }
    public TimeSpan EndHour { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    [Decompile]
    public override string ToString()
    {
        return StartHour.GetHourWithoutSeconds() + 
            " - " + 
            EndHour.GetHourWithoutSeconds();
    }
}
