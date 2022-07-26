namespace DentallApp.Features.PasswordReset;

public interface IPasswordResetService
{
    Task<Response> ResetPasswordAsync(string token, string newPassword);
    Task<Response> SendPasswordResetLinkAsync(string email);
}
