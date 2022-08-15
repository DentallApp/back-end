namespace DentallApp.Features.Roles;

public interface IRoleService
{
    Task<IEnumerable<RoleGetDto>> GetRolesAsync(bool isSuperadmin);
}
