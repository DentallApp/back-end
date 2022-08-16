namespace DentallApp.Features.Users.DTOs;

public class UserUpdatePasswordDto
{
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
}
