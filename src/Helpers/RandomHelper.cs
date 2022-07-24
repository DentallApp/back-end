namespace DentallApp.Helpers;

public class RandomHelper
{
    public static string GetRandomNumber()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}
