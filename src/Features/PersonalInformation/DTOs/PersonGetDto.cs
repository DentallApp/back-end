namespace DentallApp.Features.PersonalInformation.DTOs;

public class PersonGetDto
{
    public int PersonId { get; set; }
    public string Document { get; set; }
    public string Names { get; set; }
    public string LastNames { get; set; }
    public string CellPhone { get; set; }
    public string Email { get; set; }
}
