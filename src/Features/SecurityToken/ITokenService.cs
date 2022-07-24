namespace DentallApp.Features.SecurityToken;

public interface ITokenService
{
    string CreateJwt(IEnumerable<Claim> claims, DateTime expires, string key);
    ClaimsPrincipal ValidateJwt(string token, string key);
    ClaimsPrincipal ValidateEmailVerificationToken(string token);
    string CreateAccessToken(IEnumerable<Claim> claims);
    string CreateAccessToken(UserClaims userClaims);
    string CreateEmailVerificationToken(IEnumerable<Claim> claims);
    string CreateEmailVerificationToken(UserClaims userClaims);
    IEnumerable<Claim> CreateClaims(UserClaims userClaims);
    string CreateRefreshToken();
    ClaimsPrincipal GetPrincipalFromExpiredAccessToken(string token);
    DateTime CreateExpiryForRefreshToken();
}
