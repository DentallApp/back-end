using DentallApp.Core.Security.RefreshToken.UseCases;

namespace DentallApp.Core.Security.RefreshToken;

[Route("token")]
[ApiController]
public class RefreshTokenController
{
    [Route("refresh")]
    [HttpPost]
    public async Task<Result<CreateRefreshTokenResponse>> Create(
        [FromBody]CreateRefreshTokenRequest request,
        CreateRefreshTokenUseCase useCase)
        => await useCase.ExecuteAsync(request);

    [Route("revoke")]
    [HttpPost, Authorize]
    public async Task<Result> Revoke(
        RevokeRefreshTokenUseCase useCase)
        => await useCase.ExecuteAsync();
}
