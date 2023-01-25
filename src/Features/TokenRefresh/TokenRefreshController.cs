namespace DentallApp.Features.TokenRefresh;

[Route("token")]
[ApiController]
public class TokenRefreshController : ControllerBase
{
    private readonly ITokenRefreshService _service;

    public TokenRefreshController(ITokenRefreshService service)
    {
        _service = service;
    }

    [Route("refresh")]
    [HttpPost]
    public async Task<ActionResult<Response<TokenDto>>> Post([FromBody]TokenDto tokenDto)
    {
        var response = await _service.RefreshTokenAsync(tokenDto);
        return response.Success ? Ok(response) : BadRequest(response);
    }

    [Route("revoke")]
    [HttpPost, Authorize]
    public async Task<ActionResult<Response>> Post()
    {
        var response = await _service.RevokeTokenAsync(User.GetUserId());
        return response.Success ? Ok(response) : BadRequest(response);
    }
}
