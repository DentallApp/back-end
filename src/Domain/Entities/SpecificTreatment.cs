namespace DentallApp.Domain.Entities;

public class SpecificTreatment : EntityBase, IAuditableEntity
{
    public int GeneralTreatmentId { get; set; }
    public GeneralTreatment GeneralTreatment { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
