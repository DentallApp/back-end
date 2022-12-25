namespace DentallApp.Helpers.PasswordHasher;

public class PasswordHasherBcrypt : IPasswordHasher
{
    public string HashPassword(string text)
        => BCrypt.Net.BCrypt.HashPassword(text);

    public bool Verify(string text, string passwordHash)
        => BCrypt.Net.BCrypt.Verify(text, passwordHash);
}
