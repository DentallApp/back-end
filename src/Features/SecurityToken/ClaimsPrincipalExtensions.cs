namespace DentallApp.Features.SecurityToken;

public static class ClaimsPrincipalExtensions
{
    public static int GetUserId(this ClaimsPrincipal claims)
        => int.Parse(claims.FindFirstValue(CustomClaimsType.UserId));

    public static int GetPersonId(this ClaimsPrincipal claims)
    => int.Parse(claims.FindFirstValue(CustomClaimsType.PersonId));

    public static string GetUserName(this ClaimsPrincipal claims)
        => claims.FindFirstValue(CustomClaimsType.UserName);

    public static int GetOfficeId(this ClaimsPrincipal claims)
        => int.Parse(claims.FindFirstValue(CustomClaimsType.OfficeId));

    public static int GetEmployeeId(this ClaimsPrincipal claims)
        => int.Parse(claims.FindFirstValue(CustomClaimsType.EmployeeId));

    public static bool IsNotInOffice(this ClaimsPrincipal claims, int officeId)
        => claims.GetOfficeId() != officeId;

    public static bool IsAdmin(this ClaimsPrincipal claims)
        => claims.IsInRole(RolesName.Admin);

    /// <summary>
    /// Checks if the admin, or superadmin has not permissions to grant the roles.
    /// </summary>
    public static bool HasNotPermissions(this ClaimsPrincipal claims, IEnumerable<int> rolesId)
    {
        if(claims.IsInRole(RolesName.Admin))
        {
            foreach (int roleId in rolesId)
                if (roleId < RolesId.Secretary || roleId > RolesId.Dentist)
                    return true;
        }
        else if(claims.IsInRole(RolesName.Superadmin))
        {
            foreach (int roleId in rolesId)
                if (roleId < RolesId.Secretary || roleId > RolesId.Admin)
                    return true;
        }
        return false;
    }
}
