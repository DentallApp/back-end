namespace DentallApp.Features.Authentication;

[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [Route("login")]
    [HttpPost]
    public async Task<ActionResult<Response>> Login([FromBody]AuthPostDto authPostDto)
    {
        var response = await _authService.LoginAsync(authPostDto.UserName, authPostDto.Password);
        if(response.Success)
            return Ok(response);

        return BadRequest(response);
    }

    [Route("token/refresh")]
    [HttpPost]
    public async Task<ActionResult<Response<TokenDto>>> RefreshToken([FromBody] TokenDto tokenDto)
    {
        var response = await _authService.RefreshTokenAsync(tokenDto);
        if (response.Success)
            return Ok(response);

        return BadRequest(response);
    }

    [Route("token/revoke")]
    [HttpPost, Authorize]
    public async Task<ActionResult<Response>> RevokeToken()
    {
        var response = await _authService.RevokeRefreshTokenAsync(User.GetUserId());
        if (response.Success)
            return Ok(response);

        return NotFound(response);
    }
}
