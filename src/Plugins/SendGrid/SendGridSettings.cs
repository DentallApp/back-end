namespace DentallApp.SendGrid;

public class SendGridSettings
{
    [EnvKey("SENDGRID_API_KEY")]
    public string SendGridApiKey { get; set; }
    [EnvKey("SENDGRID_FROM_EMAIL")]
    public string SendGridFromEmail { get; set; }
    [EnvKey("SENDGRID_FROM_NAME")]
    public string SendGridFromName { get; set; }
    public string PasswordResetUrl { get; set; }
    public string EmailVerificationUrl { get; set; }
}
