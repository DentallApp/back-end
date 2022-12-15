namespace DentallApp.Entities;

public class Kinship : EntityBase
{
    public string Name { get; set; }
    public ICollection<Dependent> Dependents { get; set; }
}
