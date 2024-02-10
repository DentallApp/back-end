namespace Plugin.SendGrid;

public class EmailService(ISendGridClient client, SendGridSettings settings) : IEmailService
{
    private async Task<bool> SendEmailAsync(string recipientEmail, string recipientName, string subject, string body)
    {
        var msg = new SendGridMessage()
        {
            From = new EmailAddress(settings.SendGridFromEmail, settings.SendGridFromName),
            Subject = subject,
            HtmlContent = body
        };
        msg.AddTo(new EmailAddress(recipientEmail, recipientName));
        var response = await client.SendEmailAsync(msg);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> SendEmailForVerificationAsync(string recipientEmail, string recipientName, string token)
    {
        var confirmationLink = $"{settings.EmailVerificationUrl}?token={token}";
        var subject = $"Bienvenido {recipientName}!";
        var body = await EmailTemplateLoader.LoadTemplateForEmailVerificationAsync(confirmationLink, recipientName);
        return await SendEmailAsync(recipientEmail, recipientName, subject, body);
    }

    public async Task<bool> SendEmailForResetPasswordAsync(string recipientEmail, string recipientName, string token)
    {
        var confirmationLink = $"{settings.PasswordResetUrl}?token={token}";
        var subject = $"Restablecimiento de contraseña";
        var body = await EmailTemplateLoader.LoadTemplateForResetPasswordAsync(confirmationLink, recipientName);
        return await SendEmailAsync(recipientEmail, recipientName, subject, body);
    }
}
