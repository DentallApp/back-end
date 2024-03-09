namespace DentallApp.Infrastructure.Services.Tokens;

public class TokenService(AppSettings settings, IDateTimeService dateTimeService) : ITokenService
{
    public string CreateAccessToken(UserClaims user)
        => CreateAccessToken(CreateClaimsForUser(user));

    public string CreateAccessToken(EmployeeClaims employee)
        => CreateAccessToken(CreateClaimsForEmployee(employee));

    public string CreateAccessToken(IEnumerable<Claim> claims)
        => CreateJwt(
            claims, 
            expires: dateTimeService.UtcNow.AddMinutes(settings.AccessTokenExpires), 
            key: settings.AccessTokenKey);

    public string CreateEmailVerificationToken(IEnumerable<Claim> claims)
        => CreateJwt(
            claims, 
            expires: dateTimeService.UtcNow.AddHours(settings.EmailVerificationTokenExpires), 
            key: settings.EmailVerificationTokenKey);

    public string CreateEmailVerificationToken(UserClaims user)
        => CreateEmailVerificationToken(CreateClaimsForUser(user));

    public string CreatePasswordResetToken(int userId, string userName, string passwordHash)
    {
        var claims = new List<Claim>
        {
            new (CustomClaimsType.UserId, userId.ToString()),
            new (CustomClaimsType.UserName, userName),
         };
        var expires = dateTimeService.UtcNow.AddHours(settings.PasswordResetTokenExpires);
        return CreateJwt(claims, expires, key: passwordHash);
    }

    public string CreateRefreshToken()
        => RandomHelper.GetRandomNumber();

    public DateTime CreateExpiryForRefreshToken()
        => dateTimeService.Now.AddDays(settings.RefreshTokenExpires);

    public ClaimsPrincipal ValidateEmailVerificationToken(string token)
        => ValidateJwt(token, settings.EmailVerificationTokenKey);

    public ClaimsPrincipal ValidatePasswordResetToken(string token, string passwordHash)
        => ValidateJwt(token, key: passwordHash);

    public ClaimsPrincipal GetPrincipalFromExpiredAccessToken(string token)
        => JwtDecoder.Create()
                     .IgnoreValidateAudience()
                     .IgnoreValidateIssuer()
                     .AllowValidateIssuerSigningKey()
                     .WithIssuerSigningKey(settings.AccessTokenKey)
                     .IgnoreValidateLifetime()
                     .Decode(token);

    public ClaimsIdentity GetClaimsIdentity(string token)
    {
        try
        {
            return new ClaimsIdentity(new JwtSecurityToken(token).Claims);
        }
        catch(ArgumentException)
        {
            return null;
        }
    }

    private static string CreateJwt(IEnumerable<Claim> claims, DateTime expires, string key)
        => JwtEncoder.Create()
                     .WithSubject(claims)
                     .WithExpires(expires)
                     .WithSigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
                     .Encode();

    private static ClaimsPrincipal ValidateJwt(string token, string key)
        => JwtDecoder.Create()
                     .IgnoreValidateAudience()
                     .IgnoreValidateIssuer()
                     .AllowValidateIssuerSigningKey()
                     .WithIssuerSigningKey(key)
                     .AllowValidateLifetime()
                     .Decode(token);

    private static List<Claim> CreateClaimsForUser(UserClaims user)
    {
        var claims = new List<Claim>
        {
            new (CustomClaimsType.UserId,   user.UserId.ToString()),
            new (CustomClaimsType.PersonId, user.PersonId.ToString()),
            new (CustomClaimsType.UserName, user.UserName),
            new (CustomClaimsType.FullName, user.FullName)
         };

        foreach (var role in user.Roles)
            claims.Add(new(ClaimTypes.Role, role));

        return claims;
    }

    private static List<Claim> CreateClaimsForEmployee(EmployeeClaims employee)
    {
        var claims = CreateClaimsForUser(employee);
        claims.Add(new(CustomClaimsType.EmployeeId, employee.EmployeeId.ToString()));
        claims.Add(new(CustomClaimsType.OfficeId,   employee.OfficeId.ToString()));
        return claims;
    }
}
