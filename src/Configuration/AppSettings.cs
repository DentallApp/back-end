namespace DentallApp.Configuration;

public class AppSettings
{
    public const string MaxDateInDateInput = "MAX_DATE_IN_DATE_INPUT";
    public const string BusinessName = "BUSINESS_NAME";

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

    [EnvKey("TWILIO_ACCOUNT_SID")]
    public string TwilioAccountSid { get; set; }

    [EnvKey("TWILIO_AUTH_TOKEN")]
    public string TwilioAuthToken { get; set; }

    [EnvKey("TWILIO_FROM_NUMBER")]
    public string TwilioFromNumber { get; set; }

    [EnvKey("DEFAULT_REGION")]
    public string DefaultRegion { get; set; }

    [EnvKey("DENTAL_SERVICES_IMAGES_PATH")]
    public string DentalServicesImagesPath { get; set; }
}