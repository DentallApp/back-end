namespace DentallApp.Shared.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static int GetUserId(this ClaimsPrincipal claims)
    {
        string userId = claims.FindFirstValue(CustomClaimsType.UserId);
        return userId is null ? 
            throw new ClaimNotFoundException(CustomClaimsType.UserId) :
            int.Parse(userId);
    }

    public static string GetUserName(this ClaimsPrincipal claims)
    {
        string userName = claims.FindFirstValue(CustomClaimsType.UserName);
        return userName is null ?
            throw new ClaimNotFoundException(CustomClaimsType.UserName) :
            userName;
    }
}
