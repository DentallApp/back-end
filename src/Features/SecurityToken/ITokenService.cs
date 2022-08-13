namespace DentallApp.Features.SecurityToken;

public interface ITokenService
{
    string CreateJwt(IEnumerable<Claim> claims, DateTime expires, string key);
    ClaimsPrincipal ValidateJwt(string token, string key);
    ClaimsPrincipal ValidateEmailVerificationToken(string token);
    ClaimsPrincipal ValidatePasswordResetToken(string token, string passwordHash);
    string CreateAccessToken(IEnumerable<Claim> claims);
    string CreateAccessToken(UserClaims userClaims);
    string CreateAccessToken(EmployeeClaims employeeClaims);
    string CreateEmailVerificationToken(IEnumerable<Claim> claims);
    string CreateEmailVerificationToken(UserClaims userClaims);
    string CreatePasswordResetToken(int userid, string username, string passwordHash);
    IEnumerable<Claim> CreateClaims(UserClaims userClaims);
    IEnumerable<Claim> CreateClaims(EmployeeClaims employeeClaims);
    string CreateRefreshToken();
    ClaimsPrincipal GetPrincipalFromExpiredAccessToken(string token);
    DateTime CreateExpiryForRefreshToken();
    ClaimsIdentity GetClaimsIdentity(string token);
}
