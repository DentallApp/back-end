namespace DentallApp.Features.UserRoles;

public static class UserRoleMapper
{
    public static UserRoleDto MapToUserRoleDto(this UserRole userRole)
        => new()
        {
            RoleId = userRole.RoleId,
            RoleName = userRole.Role.Name
        };
}
