namespace DentallApp.Features.Users.DTOs;

public class UserInsertDto : UserDto
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public int GenderId { get; set; }
}
