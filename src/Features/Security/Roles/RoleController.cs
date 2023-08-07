using DentallApp.Features.Security.Roles.UseCases;

namespace DentallApp.Features.Roles;

[Route("role")]
[ApiController]
public class RoleController : ControllerBase
{
    [HttpGet("{isSuperadmin}")]
    public async Task<IEnumerable<GetRolesResponse>> GetAll(
        bool isSuperadmin,
        [FromServices]GetRolesUseCase useCase)
    { 
        return await useCase.Execute(isSuperadmin);
    }
}
