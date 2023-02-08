namespace DentallApp.Features.Authentication;

[ApiController]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [Route("login")]
    [HttpPost]
    public async Task<ActionResult<Response>> Post([FromBody]AuthPostDto authPostDto)
    {
        var response = await _authService.LoginAsync(authPostDto.UserName, authPostDto.Password);
        return response.Success ? Ok(response) : BadRequest(response);
    }
}
