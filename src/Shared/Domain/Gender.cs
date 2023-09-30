namespace DentallApp.Shared.Domain;

public class Gender : BaseEntity
{
    public string Name { get; set; }
    public ICollection<Person> Persons { get; set; }
}
