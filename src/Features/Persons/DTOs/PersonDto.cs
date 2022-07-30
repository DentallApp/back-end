namespace DentallApp.Features.Persons.DTOs;

public class PersonDto
{
    public string Document { get; set; }
    public string Names { get; set; }
    public string LastNames { get; set; }
    public string CellPhone { get; set; }
    public DateTime? DateBirth { get; set; }
    public int? GenderId { get; set; }
}
