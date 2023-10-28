using DentallApp.Features.Security.RefreshToken.UseCases;

namespace DentallApp.Features.Security.RefreshToken;

[Route("token")]
[ApiController]
public class RefreshTokenController : ControllerBase
{
    [Route("refresh")]
    [HttpPost]
    public async Task<Result<CreateRefreshTokenResponse>> Create(
        [FromBody]CreateRefreshTokenRequest request,
        [FromServices]CreateRefreshTokenUseCase useCase)
    {
        return await useCase.ExecuteAsync(request);
    }

    [Route("revoke")]
    [HttpPost, Authorize]
    public async Task<Result> Revoke(
        [FromServices]RevokeRefreshTokenUseCase useCase)
    {
        return await useCase.ExecuteAsync(User.GetUserId());
    }
}
