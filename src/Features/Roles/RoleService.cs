namespace DentallApp.Features.Roles;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _roleRepository;

    public RoleService(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<IEnumerable<RoleGetDto>> GetRolesAsync(bool isSuperadmin)
    {
        var roles = await _roleRepository.GetRolesAsync();
        if(isSuperadmin)
            return roles.Where(role =>
                                     role.Id == RolesId.Secretary ||
                                     role.Id == RolesId.Dentist ||
                                     role.Id == RolesId.Admin);

        return roles.Where(role => role.Id == RolesId.Secretary || role.Id == RolesId.Dentist);
    }
}
