using DentallApp.Core.Security.RefreshToken.UseCases;

namespace DentallApp.Core.Security.RefreshToken;

[Route("token")]
[ApiController]
public class RefreshTokenController
{
    /// <summary>
    /// Refresh an access token.
    /// </summary>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<Result>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<Result>(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType<Result>(StatusCodes.Status404NotFound)]
    [Route("refresh")]
    [HttpPost]
    public async Task<Result<CreateRefreshTokenResponse>> Create(
        [FromBody]CreateRefreshTokenRequest request,
        CreateRefreshTokenUseCase useCase)
        => await useCase.ExecuteAsync(request);

    /// <summary>
    /// Revokes the refresh token.
    /// </summary>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<Result>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<Result>(StatusCodes.Status422UnprocessableEntity)]
    [Route("revoke")]
    [HttpPost, Authorize]
    public async Task<Result> Revoke(
        RevokeRefreshTokenUseCase useCase)
        => await useCase.ExecuteAsync();
}
