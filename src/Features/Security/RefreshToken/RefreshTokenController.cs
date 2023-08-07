using DentallApp.Features.Security.RefreshToken.UseCases;

namespace DentallApp.Features.TokenRefresh;

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
        var response = await useCase.Execute(request);
        return response.Success ? Ok(response) : BadRequest(response);
    }

    [Route("revoke")]
    [HttpPost, Authorize]
    public async Task<ActionResult<Response>> Revoke(
        [FromServices]RevokeRefreshTokenUseCase useCase)
    {
        var response = await useCase.Execute(User.GetUserId());
        return response.Success ? Ok(response) : BadRequest(response);
    }
}
