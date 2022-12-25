namespace DentallApp.Helpers.PasswordHasher;

public interface IPasswordHasher
{
    bool Verify(string text, string passwordHash);
    string HashPassword(string text);
}
