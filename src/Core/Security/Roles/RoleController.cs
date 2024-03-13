using DentallApp.Core.Security.Roles.UseCases;

namespace DentallApp.Core.Security.Roles;

[Route("role")]
[ApiController]
public class RoleController
{
    /// <summary>
    /// Gets a list of role.
    /// </summary>
    [HttpGet("{isSuperadmin}")]
    public async Task<IEnumerable<GetRolesResponse>> GetAll(
        bool isSuperadmin,
        GetRolesUseCase useCase)
        => await useCase.ExecuteAsync(isSuperadmin);
}
