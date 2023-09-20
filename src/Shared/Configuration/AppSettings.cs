namespace DentallApp.Shared.Configuration;

/// <summary>
/// Represents the general configuration of the application.
/// </summary>
public class AppSettings
{
    public const string MaxDaysInCalendar = "MAX_DAYS_IN_CALENDAR";
    public const string BusinessName      = "BUSINESS_NAME";

    public string DentalServicesImagesPath { get; set; }

    public string AccessTokenKey { get; set; }
    public double AccessTokenExpires { get; set; }
    public double RefreshTokenExpires { get; set; }

    public string EmailVerificationTokenKey { get; set; }
    public double EmailVerificationTokenExpires { get; set; }
    public string EmailVerificationUrl { get; set; }

    public double PasswordResetTokenExpires { get; set; }
    public string PasswordResetUrl { get; set; }

    [EnvKey("SENDGRID_API_KEY")]
    public string SendGridApiKey { get; set; }
    [EnvKey("SENDGRID_FROM_EMAIL")]
    public string SendGridFromEmail { get; set; }
    [EnvKey("SENDGRID_FROM_NAME")]
    public string SendGridFromName { get; set; }

    public string TwilioAccountSid { get; set; }
    public string TwilioAuthToken { get; set; }
    public string TwilioFromNumber { get; set; }
    public string TwilioRegionCode { get; set; }

    public int ReminderTimeInAdvance { get; set; }
    public string ReminderCronExpr { get; set; }
}