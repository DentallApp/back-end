namespace DentallApp.Infrastructure.Services;

public class CurrentEmployeeService(ClaimsPrincipal claimsPrincipal) : ICurrentEmployee
{
    public int OfficeId
    {
        get
        {
            string officeId = claimsPrincipal.FindFirstValue(CustomClaimsType.OfficeId);
            return officeId is null ? 
                throw new InvalidOperationException("Claim type 'office_id' was not found.") : 
                int.Parse(officeId);
        }
    }

    public int EmployeeId
    {
        get
        {
            string employeeId = claimsPrincipal.FindFirstValue(CustomClaimsType.EmployeeId);
            return employeeId is null ?
                throw new InvalidOperationException("Claim type 'employee_id' was not found.") :
                int.Parse(employeeId);
        }
    }

    public string UserName
    {
        get
        {
            string userName = claimsPrincipal.FindFirstValue(CustomClaimsType.UserName);
            return userName is null ?
                throw new InvalidOperationException("Claim type 'username' was not found.") :
                userName;
        }
    }

    public bool IsNotInOffice(int officeId) 
        => OfficeId != officeId;

    public bool IsSuperAdmin()
        => claimsPrincipal.IsInRole(RoleName.Superadmin);

    public bool IsAdmin()
        => claimsPrincipal.IsInRole(RoleName.Admin);

    public bool IsDentist()
        => claimsPrincipal.IsInRole(RoleName.Dentist);

    public bool IsOnlyDentist()
        => GetClaimsRoleType().Count() == 1 && claimsPrincipal.IsInRole(RoleName.Dentist);

    public bool HasNotPermissions(IEnumerable<int> rolesId)
    {
        if (IsAdmin())
            return rolesId
                .Where(roleId => roleId < (int)Role.Predefined.Secretary || roleId > (int)Role.Predefined.Dentist)
                .Any();

        if (IsSuperAdmin())
            return rolesId
                .Where(roleId => roleId < (int)Role.Predefined.Secretary || roleId > (int)Role.Predefined.Admin)
                .Any();

        return false;
    }

    private IEnumerable<Claim> GetClaimsRoleType()
        => claimsPrincipal
            .Claims
            .Where(claim => claim.Type == ClaimTypes.Role);
}
