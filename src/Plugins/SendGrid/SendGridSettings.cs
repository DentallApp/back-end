namespace Plugin.SendGrid;

public class SendGridSettings
{
    [EnvKey("SENDGRID_API_KEY")]
    public string SendGridApiKey { get; set; } = string.Empty;
    [EnvKey("SENDGRID_FROM_EMAIL")]
    public string SendGridFromEmail { get; set; } = string.Empty;
    [EnvKey("SENDGRID_FROM_NAME")]
    public string SendGridFromName { get; set; } = string.Empty;
    public string PasswordResetUrl { get; set; } = string.Empty;
    public string EmailVerificationUrl { get; set; } = string.Empty;
}
