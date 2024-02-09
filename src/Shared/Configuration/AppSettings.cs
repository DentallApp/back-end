namespace DentallApp.Shared.Configuration;

/// <summary>
/// Represents the general configuration of the application.
/// </summary>
public class AppSettings
{
    public const string BusinessName = "BUSINESS_NAME";
    public string DentalServicesImagesPath { get; set; }
    public string Language { get; set; }

    public string AccessTokenKey { get; set; }
    public double AccessTokenExpires { get; set; }
    public double RefreshTokenExpires { get; set; }

    public string EmailVerificationTokenKey { get; set; }
    public double EmailVerificationTokenExpires { get; set; }
    public string EmailVerificationUrl { get; set; }

    public double PasswordResetTokenExpires { get; set; }
    public string PasswordResetUrl { get; set; }
}
