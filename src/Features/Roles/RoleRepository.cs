namespace DentallApp.Features.Roles;

public class RoleRepository : Repository<Role>, IRoleRepository
{
    public RoleRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<RoleGetDto>> GetRolesAsync()
        => await Context.Set<Role>()
                        .Select(role => role.MapToRoleGetDto())
                        .ToListAsync();
}
