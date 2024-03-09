namespace DentallApp.Infrastructure.Services.Tokens;

public class JwtEncoder
{
    private readonly SecurityTokenDescriptor _tokenDescriptor = new();

    private JwtEncoder()
    {

    }

    public static JwtEncoder Create() => new();

    public JwtEncoder WithSubject(IEnumerable<Claim> claims)
    {
        _tokenDescriptor.Subject = new ClaimsIdentity(claims);
        return this;
    }

    public JwtEncoder WithExpires(DateTime expires)
    {
        _tokenDescriptor.Expires = expires;
        return this;
    }

    public JwtEncoder WithSigningCredentials(string key, string algorithm, Encoding encoding = null)
    {
        encoding ??= Encoding.UTF8;
        _tokenDescriptor.SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(encoding.GetBytes(key)), algorithm);
        return this;
    }

    public string Encode()
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var createdToken = tokenHandler.CreateToken(_tokenDescriptor);
        return tokenHandler.WriteToken(createdToken);
    }
}
