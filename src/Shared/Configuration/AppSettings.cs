namespace DentallApp.Shared.Configuration;

/// <summary>
/// Represents the general configuration of the application.
/// </summary>
public class AppSettings
{
    public string BusinessName { get; set; } = string.Empty;
    public string DentalServicesImagesPath { get; set; } = string.Empty;
    public string Language { get; set; } = string.Empty;

    public string AccessTokenKey { get; set; } = string.Empty;
    public double AccessTokenExpires { get; set; }
    public double RefreshTokenExpires { get; set; }

    public string EmailVerificationTokenKey { get; set; } = string.Empty;
    public double EmailVerificationTokenExpires { get; set; }
    public string EmailVerificationUrl { get; set; } = string.Empty;

    public double PasswordResetTokenExpires { get; set; }
    public string PasswordResetUrl { get; set; } = string.Empty;
}
