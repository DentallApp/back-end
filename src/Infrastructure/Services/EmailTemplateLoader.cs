namespace DentallApp.Infrastructure.Services;

public class EmailTemplateLoader
{
    public static async Task<string> LoadTemplateForEmailVerification(string url, string recipientName)
    {
        var template = await File.ReadAllTextAsync("./Templates/EmailConfirmation.html");
        return string.Format(template, recipientName, url);
    }

    public static async Task<string> LoadTemplateForResetPassword(string url, string recipientName)
    {
        var template = await File.ReadAllTextAsync("./Templates/PasswordReset.html");
        return string.Format(template, recipientName, url);
    }
}
