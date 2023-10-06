namespace DentallApp.Shared.Extensions;

public static class ClaimsIdentityExtensions
{
    public static bool HasClaim(this ClaimsIdentity claims, string type)
        => claims.HasClaim(claim => claim.Type == type);

    public static int GetUserId(this ClaimsIdentity claims)
        => int.Parse(claims.FindFirst(CustomClaimsType.UserId).Value);

    public static string GetUserName(this ClaimsIdentity claims)
        => claims.FindFirst(CustomClaimsType.UserName).Value;
}
