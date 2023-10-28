using DentallApp.Features.FavoriteDentists.UseCases;

namespace DentallApp.Features.FavoriteDentists;

[AuthorizeByRole(RolesName.BasicUser)]
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
        [FromServices]CreateFavoriteDentistUseCase useCase)
    {
        return await useCase.ExecuteAsync(User.GetUserId(), request);
    }

    /// <summary>
    /// Deletes a favorite dentist from the current basic user.
    /// </summary>
    [HttpDelete("{favoriteDentistId}")]
    public async Task<Result> DeleteByFavoriteDentistId(
        int favoriteDentistId,
        [FromServices]DeleteFavoriteDentistUseCase useCase)
    {
        var request = new DeleteFavoriteDentistRequest
        {
            UserId = User.GetUserId(),
            FavoriteDentistId = favoriteDentistId
        };
        return await useCase.ExecuteAsync(request);
    }

    /// <summary>
    /// Deletes a favorite dentist from the current basic user.
    /// </summary>
    [HttpDelete("dentist/{dentistId}")]
    public async Task<Result> DeleteByUserIdAndDentistId(
        int dentistId,
        [FromServices]DeleteFavoriteDentistUseCase useCase)
    {
        return await useCase.ExecuteAsync(User.GetUserId(), dentistId);
    }

    /// <summary>
    /// Gets the dentists preferred by the basic user and 
    /// also includes in the result the dentists that are not preferred by the basic user.
    /// </summary>
    [HttpGet("dentist")]
    public async Task<IEnumerable<GetFavoriteAndUnfavoriteDentistsResponse>> GetFavoritesAndUnfavorites(
        [FromServices]GetFavoriteAndUnfavoriteDentistsUseCase useCase)
    {
        return await useCase.ExecuteAsync(User.GetUserId());
    }

    /// <summary>
    /// Gets only the basic user's favorite dentists.
    /// </summary>
    [HttpGet]
    public async Task<IEnumerable<GetFavoriteDentistsByUserIdResponse>> GetFavoriteDentists(
        [FromServices]GetFavoriteDentistsByUserIdUseCase useCase)
    { 
        return await useCase.ExecuteAsync(User.GetUserId());
    }
}
