namespace DentallApp.Shared.Domain;

public class Kinship : BaseEntity
{
    public string Name { get; set; }
    public ICollection<Dependent> Dependents { get; set; }
}
