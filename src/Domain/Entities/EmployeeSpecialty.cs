namespace DentallApp.Domain.Entities;

public class EmployeeSpecialty : EntityBase, IIntermediateEntity
{
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; }
    public int SpecialtyId { get; set; }
    [ForeignKey("SpecialtyId")]
    public GeneralTreatment GeneralTreatment { get; set; }

    /// <inheritdoc />
    [NotMapped]
    public int PrimaryForeignKey 
    {
        get => EmployeeId;
        set => EmployeeId = value;
    }

    /// <inheritdoc />
    [NotMapped]
    public int SecondaryForeignKey 
    {
        get => SpecialtyId;
        set => SpecialtyId = value;
    }
}
