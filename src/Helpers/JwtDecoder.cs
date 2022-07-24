namespace DentallApp.Helpers;

public class JwtDecoder
{
    private readonly TokenValidationParameters _validationParameters = new();

    private JwtDecoder()
    {

    }

    public static JwtDecoder Create() => new();

    public JwtDecoder IgnoreValidateAudience()
    {
        _validationParameters.ValidateAudience = false;
        return this;
    }

    public JwtDecoder AllowValidateAudience()
    {
        _validationParameters.ValidateAudience = true;
        return this;
    }

    public JwtDecoder IgnoreValidateIssuer()
    {
        _validationParameters.ValidateIssuer = false;
        return this;
    }

    public JwtDecoder AllowValidateIssuer()
    {
        _validationParameters.ValidateIssuer = true;
        return this;
    }

    public JwtDecoder IgnoreValidateIssuerSigningKey()
    {
        _validationParameters.ValidateIssuerSigningKey = false;
        return this;
    }

    public JwtDecoder AllowValidateIssuerSigningKey()
    {
        _validationParameters.ValidateIssuerSigningKey = true;
        return this;
    }

    public JwtDecoder IgnoreValidateLifetime()
    {
        _validationParameters.ValidateLifetime = false;
        return this;
    }

    public JwtDecoder AllowValidateLifetime()
    {
        _validationParameters.ValidateLifetime = true;
        _validationParameters.ClockSkew = TimeSpan.Zero;
        return this;
    }

    public JwtDecoder WithIssuerSigningKey(string key, Encoding encoding = null)
    {
        encoding ??= Encoding.UTF8;
        _validationParameters.IssuerSigningKey = new SymmetricSecurityKey(encoding.GetBytes(key));
        return this;
    }

    public ClaimsPrincipal Decode(string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.ValidateToken(token, _validationParameters, out var _);
        }
        catch
        {
            return null;
        }
    }
}
