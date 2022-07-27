namespace DentallApp.Features.EmailSending;

public interface IEmailTemplateService
{
    Task<string> LoadTemplateForEmailVerificationAsync(string url, string recipientName);
    Task<string> LoadTemplateForResetPasswordAsync(string url, string recipientName);
}
