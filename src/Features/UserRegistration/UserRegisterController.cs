namespace DentallApp.Features.UserRegistration;

[Route("register")]
[ApiController]
public class UserRegisterController : ControllerBase
{
    private readonly IUserRegisterService _userRegisterService;

    public UserRegisterController(IUserRegisterService userRegisterService)
    {
        _userRegisterService = userRegisterService;
    }

    [Route("basic-user")]
    [HttpPost]
    public async Task<ActionResult<Response>> CreateBasicUserAccount([FromBody]UserInsertDto userInsertDto)
    {
        var response = await _userRegisterService.CreateBasicUserAccountAsync(userInsertDto);
        if (response.Success)
            return CreatedAtAction(nameof(UserRegisterController.CreateBasicUserAccount), response);

        return BadRequest(response);
    }
}
