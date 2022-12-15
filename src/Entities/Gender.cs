namespace DentallApp.Entities;

public class Gender : EntityBase
{
    public string Name { get; set; }
    public ICollection<Person> Persons { get; set; }
}
