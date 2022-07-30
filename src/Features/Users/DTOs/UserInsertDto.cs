namespace DentallApp.Features.Users.DTOs;

public class UserInsertDto : PersonDto
{
    public string UserName { get; set; }
    public string Password { get; set; }
}
