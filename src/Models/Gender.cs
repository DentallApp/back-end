namespace DentallApp.Models;

public class Gender : ModelBase
{
    public string Name { get; set; }
    public ICollection<Person> Persons { get; set; }
}
