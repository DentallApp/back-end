namespace DentallApp.Features.Roles;

[Route("role")]
[ApiController]
public class RoleController : ControllerBase
{
    private readonly RoleService _roleService;

    public RoleController(RoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpGet("{isSuperadmin}")]
    public async Task<IEnumerable<RoleGetDto>> Get(bool isSuperadmin)
        => await _roleService.GetRolesAsync(isSuperadmin);
}
