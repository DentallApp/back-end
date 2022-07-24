namespace DentallApp.Features.EmailSending;

public interface IEmailService
{
    Task<bool> SendEmailForVerificationAsync(string recipientEmail, string recipientName, string token); 
}
