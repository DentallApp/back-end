namespace DentallApp.Features.SecurityToken;

public static class ClaimsPrincipalExtensions
{
    public static int GetUserId(this ClaimsPrincipal claims)
        => int.Parse(claims.FindFirstValue(CustomClaimsType.UserId));

    public static string GetUserName(this ClaimsPrincipal claims)
        => claims.FindFirstValue(CustomClaimsType.UserName);
}
