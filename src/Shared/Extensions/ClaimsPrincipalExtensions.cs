namespace DentallApp.Shared.Extensions;

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

    public static bool IsSuperAdmin(this ClaimsPrincipal claims)
        => claims.IsInRole(RolesName.Superadmin);

    public static bool IsAdmin(this ClaimsPrincipal claims)
        => claims.IsInRole(RolesName.Admin);

    public static bool IsDentist(this ClaimsPrincipal claims)
        => claims.IsInRole(RolesName.Dentist);

    public static bool IsOnlyDentist(this ClaimsPrincipal claims)
        => claims.GetClaimsRoleType().Count() == 1 && claims.IsInRole(RolesName.Dentist);

    private static IEnumerable<Claim> GetClaimsRoleType(this ClaimsPrincipal claims)
        => claims.Claims.Where(claim => claim.Type == ClaimTypes.Role);

    /// <summary>
    /// Compruebe si el administrador o superadministrador no tiene permisos para otorgar los nuevos roles.
    /// </summary>
    /// <param name="currentEmployee">El empleado actual al que se desea realizar la validación.</param>
    /// <param name="rolesId">Un conjunto de nuevos roles a otorgar.</param>
    /// <param name="employeeId">El ID del empleado al que se desea otorgar el nuevo rol.</param>
    /// <returns><c>true</c> si el administrador o superadministrador no tiene permisos, de lo contrario <c>false</c>.</returns>
    public static bool HasNotPermissions(this ClaimsPrincipal currentEmployee, IEnumerable<int> rolesId, int? employeeId = null)
    {
        if (currentEmployee.IsInRole(RolesName.Admin))
            return rolesId.Where(roleId => roleId < RolesId.Secretary || roleId > RolesId.Dentist).Count() > 0;

        else if (currentEmployee.IsInRole(RolesName.Superadmin))
        {
            if(employeeId == currentEmployee.GetEmployeeId())
                return rolesId.Where(roleId => 
                                     (roleId != RolesId.Superadmin) && 
                                     (roleId < RolesId.Secretary || roleId > RolesId.Admin))
                              .Count() > 0;
            
            return rolesId.Where(roleId => roleId < RolesId.Secretary || roleId > RolesId.Admin).Count() > 0;
        }

        return false;
    }
}
