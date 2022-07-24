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
    [HttpGet]
    public async Task<ActionResult<Response<UserLoginDto>>> VerifyEmailAsync([FromQuery]string token)
    {
        var response = await _service.VerifyEmailAsync(token);
        if (response.Success)
            return Ok(response);

        return Unauthorized(response);
    }
}
