namespace DentallApp.Features.Roles;

[Route("role")]
[ApiController]
public class RoleController : ControllerBase
{
    private readonly IRoleService _roleService;

    public RoleController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpGet("{isSuperadmin}")]
    public async Task<IEnumerable<RoleGetDto>> Get(bool isSuperadmin)
        => await _roleService.GetRolesAsync(isSuperadmin);
}
