namespace DentallApp.Domain.Entities;

public class SpecificTreatment : EntityBase
{
    public int GeneralTreatmentId { get; set; }
    public GeneralTreatment GeneralTreatment { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
}
