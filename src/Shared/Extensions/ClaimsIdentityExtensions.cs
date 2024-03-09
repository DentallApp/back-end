namespace DentallApp.Shared.Extensions;

public static class ClaimsIdentityExtensions
{
    public static bool HasClaim(this ClaimsIdentity claims, string type)
        => claims.HasClaim(claim => claim.Type == type);

    public static int GetUserId(this ClaimsIdentity claims)
    {
        Claim claim = claims.FindFirst(CustomClaimsType.UserId);
        return claim is null ?
            throw new ClaimNotFoundException(CustomClaimsType.UserId) :
            int.Parse(claim.Value);

    }

    public static string GetUserName(this ClaimsIdentity claims)
    {
        Claim claim = claims.FindFirst(CustomClaimsType.UserName);
        return claim is null ?
            throw new ClaimNotFoundException(CustomClaimsType.UserName) :
            claim.Value;
    }
}
