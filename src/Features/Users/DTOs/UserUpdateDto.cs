namespace DentallApp.Features.Users.DTOs;

public class UserUpdateDto
{
    public string Names { get; set; }
    public string LastNames { get; set; }
    public string CellPhone { get; set; }
    public DateTime DateBirth { get; set; }
    public int GenderId { get; set; }
}
