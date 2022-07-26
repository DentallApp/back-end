namespace DentallApp.Features.EmailVerification;

[ApiController]
public class EmailVerificationController : ControllerBase
{
    private readonly IEmailVerificationService _service;

    public EmailVerificationController(IEmailVerificationService service)
    {
        _service = service;
    }

    [Route("email-verification")]
    [HttpPost]
    public async Task<ActionResult<Response<UserLoginDto>>> VerifyEmailAsync([FromBody]EmailVerificationDto emailDto)
    {
        var response = await _service.VerifyEmailAsync(emailDto.Token);
        if (response.Success)
            return Ok(response);

        return Unauthorized(response);
    }
}
