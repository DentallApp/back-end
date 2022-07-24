namespace DentallApp.Features.Users.DTOs;

public class UserDto
{
    public string Document { get; set; }
    public string Names { get; set; }
    public string LastNames { get; set; }
    public string CellPhone { get; set; }
    public DateTime? DateBirth { get; set; }
}
