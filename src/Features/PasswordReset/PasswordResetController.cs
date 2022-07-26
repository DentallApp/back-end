namespace DentallApp.Features.PasswordReset;

[ApiController]
public class PasswordResetController : ControllerBase
{
    private readonly IPasswordResetService _service;

    public PasswordResetController(IPasswordResetService service)
    {
        _service = service;
    }

    [Route("password-reset")]
    [HttpPost]
    public async Task<ActionResult<Response>> ResetPasswordAsync(PasswordResetDto passwordDto)
    {
        var response = await _service.ResetPasswordAsync(passwordDto.Token, passwordDto.NewPassword);
        if (response.Success)
            return Ok(response);

        return BadRequest(response);
    }

    [Route("password-reset/send")]
    [HttpPost]
    public async Task<ActionResult<Response>> SendPasswordResetLinkAsync(PasswordResetSendDto passwordDto)
    {
        var response = await _service.SendPasswordResetLinkAsync(passwordDto.Email);
        if (response.Success)
            return Ok(response);

        return BadRequest(response);
    }
}
