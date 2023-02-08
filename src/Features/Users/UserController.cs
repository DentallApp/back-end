namespace DentallApp.Features.Users;

[Authorize]
[Route("user")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPut]
    public async Task<ActionResult<Response>> Put([FromBody]UserUpdateDto userUpdateDto)
    {
        var response = await _userService.EditUserProfileAsync(User.GetPersonId(), userUpdateDto);
        return response.Success ? Ok(response) : BadRequest(response);
    }

    [Route("password")]
    [HttpPut]
    public async Task<ActionResult<Response>> Put([FromBody]UserUpdatePasswordDto userUpdatePasswordDto)
    {
        var response = await _userService.ChangePasswordAsync(User.GetUserId(), userUpdatePasswordDto);
        return response.Success ? Ok(response) : BadRequest(response);
    }
}
