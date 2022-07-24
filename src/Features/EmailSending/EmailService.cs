namespace DentallApp.Features.EmailSending;

public class EmailService : IEmailService
{
    private readonly ISendGridClient _client;
    private readonly IEmailTemplateService _emailTemplate;
    private readonly AppSettings _settings;

    public EmailService(ISendGridClient client, IEmailTemplateService emailTemplate, AppSettings settings)
    {
        _client = client;
        _settings = settings;
        _emailTemplate = emailTemplate;
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
        var confirmationLink = $"{_settings.BaseUrl}?token={token}";
        var subject = $"Bienvenido {recipientName}!";
        var body = await _emailTemplate.LoadTemplateForEmailVerification(confirmationLink, recipientName);
        return await SendEmailAsync(recipientEmail, recipientName, subject, body);
    }
}
