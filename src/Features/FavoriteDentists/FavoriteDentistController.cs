using DentallApp.Features.FavoriteDentists.UseCases;

namespace DentallApp.Features.FavoriteDentists;

[AuthorizeByRole(RoleName.BasicUser)]
[Route("favorite-dentist")]
[ApiController]
public class FavoriteDentistController : ControllerBase
{
    /// <summary>
    /// Creates a new favorite dentist for the current basic user.
    /// </summary>
    [HttpPost]
    public async Task<Result<CreatedId>> Create(
        [FromBody]CreateFavoriteDentistRequest request,
        CreateFavoriteDentistUseCase useCase)
        => await useCase.ExecuteAsync(User.GetUserId(), request);

    /// <summary>
    /// Deletes a favorite dentist from the current basic user.
    /// </summary>
    [HttpDelete("{favoriteDentistId}")]
    public async Task<Result> DeleteByFavoriteDentistId(
        int favoriteDentistId,
        DeleteFavoriteDentistUseCase useCase)
        => await useCase.ExecuteAsync(new DeleteFavoriteDentistRequest
        {
            UserId = User.GetUserId(),
            FavoriteDentistId = favoriteDentistId
        });

    /// <summary>
    /// Deletes a favorite dentist from the current basic user.
    /// </summary>
    [HttpDelete("dentist/{dentistId}")]
    public async Task<Result> DeleteByUserIdAndDentistId(
        int dentistId,
        DeleteFavoriteDentistUseCase useCase)
        => await useCase.ExecuteAsync(User.GetUserId(), dentistId);

    /// <summary>
    /// Gets the dentists preferred by the basic user and 
    /// also includes in the result the dentists that are not preferred by the basic user.
    /// </summary>
    [HttpGet("dentist")]
    public async Task<IEnumerable<GetFavoriteAndUnfavoriteDentistsResponse>> GetFavoritesAndUnfavorites(
        GetFavoriteAndUnfavoriteDentistsUseCase useCase)
        => await useCase.ExecuteAsync(User.GetUserId());

    /// <summary>
    /// Gets only the basic user's favorite dentists.
    /// </summary>
    [HttpGet]
    public async Task<IEnumerable<GetFavoriteDentistsByUserIdResponse>> GetFavoriteDentists(
        GetFavoriteDentistsByUserIdUseCase useCase)
        => await useCase.ExecuteAsync(User.GetUserId());
}
