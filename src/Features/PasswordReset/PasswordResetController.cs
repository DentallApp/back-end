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
    public async Task<ActionResult<Response>> ResetPassword(PasswordResetDto passwordDto)
    {
        var response = await _service.ResetPasswordAsync(passwordDto.Token, passwordDto.NewPassword);
        return response.Success ? Ok(response) : BadRequest(response);
    }

    [Route("password-reset/send")]
    [HttpPost]
    public async Task<ActionResult<Response>> SendPasswordResetLink(PasswordResetSendDto passwordDto)
    {
        var response = await _service.SendPasswordResetLinkAsync(passwordDto.Email);
        return response.Success ? Ok(response) : BadRequest(response);
    }
}
