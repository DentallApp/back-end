namespace DentallApp.Features.Persons;

public class Person : ModelBase
{
    public string Document { get; set; }
    public string Names { get; set; }
    public string LastNames { get; set; }
    public string CellPhone { get; set; }
    public string Email { get; set; }
    public DateTime? DateBirth { get; set; }
    public int? GenderId { get; set; }
    public Gender Gender { get; set; }
    public User User { get; set; }
    public Employee Employee { get; set; }
    public Dependent Dependent { get; set; }

    [NotMapped]
    public string FullName => Names + " " + LastNames;
}
