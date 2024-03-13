using DentallApp.Core.FavoriteDentists.UseCases;

namespace DentallApp.Core.FavoriteDentists;

[AuthorizeByRole(RoleName.BasicUser)]
[Route("favorite-dentist")]
[ApiController]
public class FavoriteDentistController
{
    /// <summary>
    /// Creates a new favorite dentist for the current basic user.
    /// </summary>
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType<Result>(StatusCodes.Status400BadRequest)]
    [HttpPost]
    public async Task<Result<CreatedId>> Create(
        [FromBody]CreateFavoriteDentistRequest request,
        CreateFavoriteDentistUseCase useCase)
        => await useCase.ExecuteAsync(request);

    /// <summary>
    /// Deletes a favorite dentist from the current basic user by ID.
    /// </summary>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<Result>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<Result>(StatusCodes.Status403Forbidden)]
    [HttpDelete("{favoriteDentistId}")]
    public async Task<Result> DeleteByFavoriteDentistId(
        int favoriteDentistId,
        DeleteFavoriteDentistUseCase useCase)
        => await useCase.ExecuteAsync(new FavoriteDentistId { Value = favoriteDentistId });

    /// <summary>
    /// Deletes a favorite dentist from the current basic user by DentistId.
    /// </summary>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<Result>(StatusCodes.Status404NotFound)]
    [HttpDelete("dentist/{dentistId}")]
    public async Task<Result> DeleteByDentistId(
        int dentistId,
        DeleteFavoriteDentistUseCase useCase)
        => await useCase.ExecuteAsync(dentistId);

    /// <summary>
    /// Gets the dentists preferred by the basic user and 
    /// also includes in the result the dentists that are not preferred by the basic user.
    /// </summary>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet("dentist")]
    public async Task<IEnumerable<GetFavoriteAndUnfavoriteDentistsResponse>> GetFavoritesAndUnfavorites(
        GetFavoriteAndUnfavoriteDentistsUseCase useCase)
        => await useCase.ExecuteAsync();

    /// <summary>
    /// Gets only the basic user's favorite dentists.
    /// </summary>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet]
    public async Task<IEnumerable<GetFavoriteDentistsByCurrentUserIdResponse>> GetFavoriteDentistsByCurrentUserId(
        GetFavoriteDentistsByCurrentUserIdUseCase useCase)
        => await useCase.ExecuteAsync();
}
