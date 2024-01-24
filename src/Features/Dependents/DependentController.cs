using DentallApp.Features.Dependents.UseCases;

namespace DentallApp.Features.Dependents;

[AuthorizeByRole(RolesName.BasicUser)]
[Route("dependent")]
[ApiController]
public class DependentController : ControllerBase
{
    [HttpPost]
    public async Task<Result<CreatedId>> Create(
        [FromBody]CreateDependentRequest request,
        CreateDependentUseCase useCase)
        => await useCase.ExecuteAsync(User.GetUserId(), request);

    [HttpDelete("{id}")]
    public async Task<Result> Delete(
        int id,
        DeleteDependentUseCase useCase)
        => await useCase.ExecuteAsync(id, User.GetUserId());

    [HttpPut("{id}")]
    public async Task<Result> Update(
        int id,
        [FromBody]UpdateDependentRequest request,
        UpdateDependentUseCase useCase)
        => await useCase.ExecuteAsync(id, User.GetUserId(), request);

    [Route("user")]
    [HttpGet]
    public async Task<IEnumerable<GetDependentsByUserIdResponse>> GetByUserId(
        GetDependentsByUserIdUseCase useCase)
        => await useCase.ExecuteAsync(User.GetUserId());
}
