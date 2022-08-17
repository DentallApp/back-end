namespace DentallApp.Features.SecurityToken;

public interface ITokenService
{
    ClaimsPrincipal ValidateEmailVerificationToken(string token);
    ClaimsPrincipal ValidatePasswordResetToken(string token, string passwordHash);
    string CreateAccessToken(IEnumerable<Claim> claims);
    string CreateAccessToken(UserClaims userClaims);
    string CreateAccessToken(EmployeeClaims employeeClaims);
    string CreateEmailVerificationToken(IEnumerable<Claim> claims);
    string CreateEmailVerificationToken(UserClaims userClaims);
    string CreatePasswordResetToken(int userid, string username, string passwordHash);
    string CreateRefreshToken();
    DateTime CreateExpiryForRefreshToken();
    ClaimsPrincipal GetPrincipalFromExpiredAccessToken(string token);
    ClaimsIdentity GetClaimsIdentity(string token);
}
