namespace DentallApp.Features.SpecificTreatments;

public class SpecificTreatment : ModelBase
{
    public int GeneralTreatmentId { get; set; }
    public GeneralTreatment GeneralTreatment { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
}
