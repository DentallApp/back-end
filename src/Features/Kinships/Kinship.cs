namespace DentallApp.Features.Kinships;

public class Kinship : ModelBase
{
    public string Name { get; set; }
    public ICollection<Dependent> Dependents { get; set; }
}
