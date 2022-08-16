namespace DentallApp.Features.GeneralTreatments.DTOs;

public class GeneralTreatmentUpdateDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public IFormFile Image { get; set; }
    public int Duration { get; set; }
}
