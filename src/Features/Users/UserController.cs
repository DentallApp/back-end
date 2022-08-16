namespace DentallApp.Features.Users;

[Authorize]
[Route("user")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPut]
    public async Task<ActionResult<Response>> Put([FromBody]UserUpdateDto userUpdateDto)
    {
        var response = await _userService.EditUserProfileAsync(User.GetPersonId(), userUpdateDto);
        if (response.Success)
            return Ok(response);

        return BadRequest(response);
    }

    [Route("password")]
    [HttpPut]
    public async Task<ActionResult<Response>> Put([FromBody]UserUpdatePasswordDto userUpdatePasswordDto)
    {
        var response = await _userService.ChangePasswordAsync(User.GetUserId(), userUpdatePasswordDto);
        if (response.Success)
            return Ok(response);

        return BadRequest(response);
    }
}
