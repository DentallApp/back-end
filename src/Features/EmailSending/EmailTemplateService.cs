namespace DentallApp.Features.EmailSending;

public class EmailTemplateService : IEmailTemplateService
{
    public async Task<string> LoadTemplateForEmailVerification(string url, string recipientName)
    {
        var template = await File.ReadAllTextAsync("./Templates/EmailConfirmation.html");
        var templateBody = string.Format(template, recipientName, url);
        var templateHead = await File.ReadAllTextAsync("./Templates/EmailConfirmationStyles.html");
        return templateHead + templateBody;
    }
}
