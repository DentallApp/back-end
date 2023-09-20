namespace DentallApp.Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly ISendGridClient _client;
    private readonly AppSettings _settings;

    public EmailService(ISendGridClient client, AppSettings settings)
    {
        _client = client;
        _settings = settings;
    }

    private async Task<bool> SendEmailAsync(string recipientEmail, string recipientName, string subject, string body)
    {
        var msg = new SendGridMessage()
        {
            From = new EmailAddress(_settings.SendGridFromEmail, _settings.SendGridFromName),
            Subject = subject,
            HtmlContent = body
        };
        msg.AddTo(new EmailAddress(recipientEmail, recipientName));
        var response = await _client.SendEmailAsync(msg);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> SendEmailForVerificationAsync(string recipientEmail, string recipientName, string token)
    {
        var confirmationLink = $"{_settings.EmailVerificationUrl}?token={token}";
        var subject = $"Bienvenido {recipientName}!";
        var body = await EmailTemplateLoader.LoadTemplateForEmailVerification(confirmationLink, recipientName);
        return await SendEmailAsync(recipientEmail, recipientName, subject, body);
    }

    public async Task<bool> SendEmailForResetPasswordAsync(string recipientEmail, string recipientName, string token)
    {
        var confirmationLink = $"{_settings.PasswordResetUrl}?token={token}";
        var subject = $"Restablecimiento de contraseña";
        var body = await EmailTemplateLoader.LoadTemplateForResetPassword(confirmationLink, recipientName);
        return await SendEmailAsync(recipientEmail, recipientName, subject, body);
    }
}
