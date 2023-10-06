using DentallApp.Features.Security.RefreshToken.UseCases;

namespace DentallApp.Features.Security.RefreshToken;

[Route("token")]
[ApiController]
public class RefreshTokenController : ControllerBase
{
    [Route("refresh")]
    [HttpPost]
    public async Task<ActionResult<Response<CreateRefreshTokenResponse>>> Create(
        [FromBody]CreateRefreshTokenRequest request,
        [FromServices]CreateRefreshTokenUseCase useCase)
    {
        var response = await useCase.ExecuteAsync(request);
        return response.Success ? Ok(response) : BadRequest(response);
    }

    [Route("revoke")]
    [HttpPost, Authorize]
    public async Task<ActionResult<Response>> Revoke(
        [FromServices]RevokeRefreshTokenUseCase useCase)
    {
        var response = await useCase.ExecuteAsync(User.GetUserId());
        return response.Success ? Ok(response) : BadRequest(response);
    }
}
