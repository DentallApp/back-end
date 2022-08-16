namespace DentallApp.Features.Roles;

public interface IRoleRepository : IRepository<Role>
{
    Task<IEnumerable<RoleGetDto>> GetRolesAsync();
}
