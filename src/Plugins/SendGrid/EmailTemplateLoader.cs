namespace Plugin.SendGrid;

public class EmailTemplateLoader
{
    public static async Task<string> LoadTemplateForEmailVerificationAsync(string url, string recipientName)
    {
        var template = await File.ReadAllTextAsync("./Templates/EmailConfirmation.html");
        return string.Format(template, recipientName, url);
    }

    public static async Task<string> LoadTemplateForResetPasswordAsync(string url, string recipientName)
    {
        var template = await File.ReadAllTextAsync("./Templates/PasswordReset.html");
        return string.Format(template, recipientName, url);
    }
}
