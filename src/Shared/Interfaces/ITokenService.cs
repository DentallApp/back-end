namespace DentallApp.Shared.Interfaces;

public interface ITokenService
{
    string CreateAccessToken(UserClaims user);
    string CreateAccessToken(EmployeeClaims employee);
    string CreateAccessToken(IEnumerable<Claim> claims);
    string CreateEmailVerificationToken(IEnumerable<Claim> claims);
    string CreateEmailVerificationToken(UserClaims user);
    string CreatePasswordResetToken(int userId, string userName, string passwordHash);
    string CreateRefreshToken();
    DateTime CreateExpiryForRefreshToken();
    ClaimsPrincipal ValidateEmailVerificationToken(string token);
    ClaimsPrincipal ValidatePasswordResetToken(string token, string passwordHash);
    ClaimsPrincipal GetPrincipalFromExpiredAccessToken(string token);
    ClaimsIdentity GetClaimsIdentity(string token);
}
