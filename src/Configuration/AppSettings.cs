namespace DentallApp.Configuration;

public class AppSettings
{
    [EnvKey("CONNECTION_STRING")]
    public string ConnectionString { get; set; }

    [EnvKey("ACCESS_TOKEN_KEY")]
    public string AccessTokenKey { get; set; }

    [EnvKey("ACCESS_TOKEN_EXPIRES")]
    public double AccessTokenExpires { get; set; }

    [EnvKey("EMAIL_VERIFICATION_TOKEN_KEY")]
    public string EmailVerificationTokenKey { get; set; }

    [EnvKey("EMAIL_VERIFICATION_TOKEN_EXPIRES")]
    public double EmailVerificationTokenExpires { get; set; }

    [EnvKey("EMAIL_VERIFICATION_URL")]
    public string EmailVerificationUrl { get; set; }

    [EnvKey("PASSWORD_RESET_TOKEN_EXPIRES")]
    public double PasswordResetTokenExpires { get; set; }

    [EnvKey("PASSWORD_RESET_URL")]
    public string PasswordResetUrl { get; set; }

    [EnvKey("REFRESH_TOKEN_EXPIRES")]
    public double RefreshTokenExpires { get; set; }

    [EnvKey("SENDGRID_API_KEY")]
    public string SendGridApiKey { get; set; }

    [EnvKey("SENDGRID_FROM_EMAIL")]
    public string SendGridFromEmail { get; set; }

    [EnvKey("SENDGRID_FROM_NAME")]
    public string SendGridFromName { get; set; }
}