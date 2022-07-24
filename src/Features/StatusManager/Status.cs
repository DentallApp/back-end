namespace DentallApp.Features.StatusManager;

public class Status : ModelBase
{
    public string Name { get; set; }
    public ICollection<GeneralTreatment> GeneralTreatments { get; set; }
}
