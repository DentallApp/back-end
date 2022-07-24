namespace DentallApp.Features.Genders;

public class Gender : ModelBase
{
    public string Name { get; set; }
    public ICollection<Person> Persons { get; set; }
}
