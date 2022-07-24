namespace DentallApp.Features.GeneralTreatments;

public class GeneralTreatment : ModelWithStatus
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public int Duration { get; set; }
}