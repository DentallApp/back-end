namespace DentallApp.Features.SpecificTreatments.DTOs;

public class SpecificTreatmentInsertDto
{
    public string Name { get; set; }
    public int GeneralTreatmentId { get; set; }
    public double Price { get; set; }
}
