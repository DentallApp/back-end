namespace DentallApp.Features.PasswordReset.DTOs;

public class PasswordResetDto
{
    public string Token { get; set; }
    public string NewPassword { get; set; }
}
