namespace DentallApp.Infrastructure.Services;

public class FakeEmailService(ILogger<FakeEmailService> logger, AppSettings settings) : IEmailService
{
    private const string Template =
    """
    Recipient Email: {recipientEmail}
    Recipient Name: {recipientName}
    Confirmation Link: 
    {confirmationLink}
    """;

    public Task<bool> SendEmailForResetPasswordAsync(string recipientEmail, string recipientName, string token)
    {
        var confirmationLink = $"{settings.PasswordResetUrl}?token={token}";
        logger.LogInformation(Template, recipientEmail, recipientName, confirmationLink);
        return Task.FromResult(true);
    }

    public Task<bool> SendEmailForVerificationAsync(string recipientEmail, string recipientName, string token)
    {
        var confirmationLink = $"{settings.EmailVerificationUrl}?token={token}";
        logger.LogInformation(Template, recipientEmail, recipientName, confirmationLink);
        return Task.FromResult(true);
    }
}
