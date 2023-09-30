namespace DentallApp.Shared.Domain;

public class PublicHoliday : SoftDeleteEntity, IAuditableEntity
{
    public string Description { get; set; }
    public int Day { get; set; }
    public int Month { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public List<OfficeHoliday> Offices { get; set; }
}
