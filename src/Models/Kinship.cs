namespace DentallApp.Models;

public class Kinship : ModelBase
{
    public string Name { get; set; }
    public ICollection<Dependent> Dependents { get; set; }
}
