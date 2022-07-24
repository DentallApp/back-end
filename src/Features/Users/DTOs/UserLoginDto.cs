namespace DentallApp.Features.Users.DTOs;

public class UserLoginDto : FullUserProfileDto
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}
