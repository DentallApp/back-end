namespace DentallApp.Features.SpecificTreatments.DTOs;

public class SpecificTreatmentShowDto
{
    public int SpecificTreatmentId { get; set; }
    public string SpecificTreatmentName { get; set; }
    public int GeneralTreatmentId { get; set; }
    public string GeneralTreatmentName { get; set; }
    public double Price { get; set; }
}
