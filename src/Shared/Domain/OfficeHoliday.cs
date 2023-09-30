namespace DentallApp.Shared.Domain;

public class OfficeHoliday : 
    BaseEntity, 
    IAuditableEntity,
    IIntermediateEntity
{
    public int PublicHolidayId { get; set; }
    public PublicHoliday PublicHoliday { get; set; }
    public int OfficeId { get; set; }
    public Office Office { get; set; }

    /// <inheritdoc />
    [NotMapped]
    public int PrimaryForeignKey 
    {
        get => PublicHolidayId;
        set => PublicHolidayId = value;
    }

    /// <inheritdoc />
    [NotMapped]
    public int SecondaryForeignKey 
    {
        get => OfficeId;
        set => OfficeId = value;
    }

    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
