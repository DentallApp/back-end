using DentallApp.Core.Dependents.UseCases;

namespace DentallApp.Core.Dependents;

[AuthorizeByRole(RoleName.BasicUser)]
[Route("dependent")]
[ApiController]
public class DependentController
{
    /// <summary>
    /// Creates a dependent for a specific basic user.
    /// </summary>
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType<Result>(StatusCodes.Status400BadRequest)]
    [HttpPost]
    public async Task<Result<CreatedId>> Create(
        [FromBody]CreateDependentRequest request,
        CreateDependentUseCase useCase)
        => await useCase.ExecuteAsync(request);

    /// <summary>
    /// Deletes a dependent of a basic user by ID.
    /// </summary>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<Result>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<Result>(StatusCodes.Status403Forbidden)]
    [HttpDelete("{id}")]
    public async Task<Result> Delete(
        int id,
        DeleteDependentUseCase useCase)
        => await useCase.ExecuteAsync(id);

    /// <summary>
    /// Updates a dependent of a basic user by ID.
    /// </summary>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<Result>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<Result>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<Result>(StatusCodes.Status403Forbidden)]
    [HttpPut("{id}")]
    public async Task<Result> Update(
        int id,
        [FromBody]UpdateDependentRequest request,
        UpdateDependentUseCase useCase)
        => await useCase.ExecuteAsync(id, request);

    /// <summary>
    /// Gets the dependents of the current basic user.
    /// </summary>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Route("user")]
    [HttpGet]
    public async Task<IEnumerable<GetDependentsByCurrentUserIdResponse>> GetByCurrentUserId(
        GetDependentsByCurrentUserIdUseCase useCase)
        => await useCase.ExecuteAsync();
}
