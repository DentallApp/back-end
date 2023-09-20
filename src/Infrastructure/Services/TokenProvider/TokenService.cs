namespace DentallApp.Infrastructure.Services.TokenProvider;

public class TokenService : ITokenService
{
    private readonly AppSettings _settings;
    private readonly IDateTimeProvider _dateTimeProvider;

    public TokenService(AppSettings settings, IDateTimeProvider dateTimeProvider)
    {
        _settings = settings;
        _dateTimeProvider = dateTimeProvider;
    }

    private string CreateJwt(IEnumerable<Claim> claims, DateTime expires, string key)
        => JwtEncoder.Create()
                     .WithSubject(claims)
                     .WithExpires(expires)
                     .WithSigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
                     .Encode();

    private ClaimsPrincipal ValidateJwt(string token, string key)
        => JwtDecoder.Create()
                     .IgnoreValidateAudience()
                     .IgnoreValidateIssuer()
                     .AllowValidateIssuerSigningKey()
                     .WithIssuerSigningKey(key)
                     .AllowValidateLifetime()
                     .Decode(token);

    public ClaimsPrincipal ValidateEmailVerificationToken(string token)
        => ValidateJwt(token, _settings.EmailVerificationTokenKey);

    public ClaimsPrincipal ValidatePasswordResetToken(string token, string passwordHash)
        => ValidateJwt(token, key: passwordHash);

    public string CreateAccessToken(IEnumerable<Claim> claims)
        => CreateJwt(claims, _dateTimeProvider.UtcNow.AddMinutes(_settings.AccessTokenExpires), _settings.AccessTokenKey);

    public string CreateAccessToken(UserClaims userClaims)
        => CreateAccessToken(CreateClaims(userClaims));

    public string CreateAccessToken(EmployeeClaims employeeClaims)
        => CreateAccessToken(CreateClaims(employeeClaims));

    public string CreateEmailVerificationToken(IEnumerable<Claim> claims)
        => CreateJwt(claims, _dateTimeProvider.UtcNow.AddHours(_settings.EmailVerificationTokenExpires), _settings.EmailVerificationTokenKey);

    public string CreateEmailVerificationToken(UserClaims userClaims)
        => CreateEmailVerificationToken(CreateClaims(userClaims));

    public string CreatePasswordResetToken(int userid, string username, string passwordHash)
    {
        var claims = new List<Claim>
        {
            new (CustomClaimsType.UserId, userid.ToString()),
            new (CustomClaimsType.UserName, username),
         };
        var expires = _dateTimeProvider.UtcNow.AddHours(_settings.PasswordResetTokenExpires);
        return CreateJwt(claims, expires, key: passwordHash);
    }

    private IEnumerable<Claim> CreateClaims(UserClaims userClaims)
    {
        var claims = new List<Claim>
        {
            new (CustomClaimsType.UserId, userClaims.UserId.ToString()),
            new (CustomClaimsType.PersonId, userClaims.PersonId.ToString()),
            new (CustomClaimsType.UserName, userClaims.UserName),
            new (CustomClaimsType.FullName, userClaims.FullName)
         };

        foreach (var role in userClaims.Roles)
            claims.Add(new(ClaimTypes.Role, role));

        return claims;
    }

    private IEnumerable<Claim> CreateClaims(EmployeeClaims employeeClaims)
    {
        var claims = CreateClaims((UserClaims)employeeClaims) as List<Claim>;
        claims.Add(new (CustomClaimsType.EmployeeId, employeeClaims.EmployeeId.ToString()));
        claims.Add(new (CustomClaimsType.OfficeId, employeeClaims.OfficeId.ToString()));
        return claims;
    }

    public string CreateRefreshToken()
        => RandomHelper.GetRandomNumber();

    public DateTime CreateExpiryForRefreshToken()
        => _dateTimeProvider.Now.AddDays(_settings.RefreshTokenExpires);

    public ClaimsPrincipal GetPrincipalFromExpiredAccessToken(string token)
        => JwtDecoder.Create()
                     .IgnoreValidateAudience()
                     .IgnoreValidateIssuer()
                     .AllowValidateIssuerSigningKey()
                     .WithIssuerSigningKey(_settings.AccessTokenKey)
                     .IgnoreValidateLifetime()
                     .Decode(token);

    public ClaimsIdentity GetClaimsIdentity(string token)
    {
        try
        {
            return new (new JwtSecurityToken(token).Claims);
        }
        catch(ArgumentException)
        {
            return null;
        }
    }
}
