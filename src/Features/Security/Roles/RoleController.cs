using DentallApp.Features.Security.Roles.UseCases;

namespace DentallApp.Features.Security.Roles;

[Route("role")]
[ApiController]
public class RoleController
{
    [HttpGet("{isSuperadmin}")]
    public async Task<IEnumerable<GetRolesResponse>> GetAll(
        bool isSuperadmin,
        GetRolesUseCase useCase)
        => await useCase.ExecuteAsync(isSuperadmin);
}
