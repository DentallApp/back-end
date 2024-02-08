namespace DentallApp.Shared.Interfaces;

public interface IEmailService
{
    Task<bool> SendEmailForVerificationAsync(string recipientEmail, string recipientName, string token);
    Task<bool> SendEmailForResetPasswordAsync(string recipientEmail, string recipientName, string token);
}
