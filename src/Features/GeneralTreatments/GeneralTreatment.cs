namespace DentallApp.Features.GeneralTreatments;

public class GeneralTreatment : ModelBase
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public int EstimatedTime { get; set; }
}