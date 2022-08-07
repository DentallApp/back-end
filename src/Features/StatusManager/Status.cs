namespace DentallApp.Features.StatusManager;

public class Status : ModelBase
{
    public string Name { get; set; }
    public ICollection<GeneralTreatment> GeneralTreatments { get; set; }
    public ICollection<Dependent> Dependents { get; set; }
    public ICollection<Employee> Employees { get; set; }
    public ICollection<Office> Offices { get; set; }
}
