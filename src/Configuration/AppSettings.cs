namespace DentallApp.Configuration;

public class AppSettings
{
    [EnvKey("BASE_URL")]
    public string BaseUrl { get; set; }

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

    [EnvKey("REFRESH_TOKEN_EXPIRES")]
    public double RefreshTokenExpires { get; set; }

    [EnvKey("SENDGRID_API_KEY")]
    public string SendGridApiKey { get; set; }

    [EnvKey("SENDGRID_FROM_EMAIL")]
    public string SendGridFromEmail { get; set; }

    [EnvKey("SENDGRID_FROM_NAME")]
    public string SendGridFromName { get; set; }
}