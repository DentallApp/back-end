namespace DentallApp.Features.Users.DTOs;

public class FullUserProfileDto : PersonDto
{
    public int UserId { get; set; }
    public string UserName { get; set; }
    public IEnumerable<string> Roles { get; set; }
    public string GenderName { get; set; }
}
