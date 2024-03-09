using DentallApp.Core.Dependents.UseCases;

namespace DentallApp.Core.Dependents;

[AuthorizeByRole(RoleName.BasicUser)]
[Route("dependent")]
[ApiController]
public class DependentController
{
    [HttpPost]
    public async Task<Result<CreatedId>> Create(
        [FromBody]CreateDependentRequest request,
        CreateDependentUseCase useCase)
        => await useCase.ExecuteAsync(request);

    [HttpDelete("{id}")]
    public async Task<Result> Delete(
        int id,
        DeleteDependentUseCase useCase)
        => await useCase.ExecuteAsync(id);

    [HttpPut("{id}")]
    public async Task<Result> Update(
        int id,
        [FromBody]UpdateDependentRequest request,
        UpdateDependentUseCase useCase)
        => await useCase.ExecuteAsync(id, request);

    [Route("user")]
    [HttpGet]
    public async Task<IEnumerable<GetDependentsByCurrentUserIdResponse>> GetByCurrentUserId(
        GetDependentsByCurrentUserIdUseCase useCase)
        => await useCase.ExecuteAsync();
}
