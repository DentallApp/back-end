namespace DentallApp.Features.EmailSending;

public interface IEmailTemplateService
{
    Task<string> LoadTemplateForEmailVerification(string url, string recipientName);
    Task<string> LoadTemplateForResetPassword(string url, string recipientName);
}
