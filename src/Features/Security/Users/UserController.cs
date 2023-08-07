using DentallApp.Features.Security.Users.UseCases;

namespace DentallApp.Features.Users;

[Authorize]
[Route("user")]
[ApiController]
public class UserController : ControllerBase
{
    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult<Response>> Create(
        [FromBody]CreateBasicUserRequest request,
        [FromServices]CreateBasicUserUseCase useCase)
    {
        var response = await useCase.Execute(request);
        return response.Success ?
               CreatedAtAction(nameof(Create), response) :
               BadRequest(response);
    }

    [AllowAnonymous]
    [Route("login")]
    [HttpPost]
    public async Task<ActionResult<Response>> Login(
        [FromBody]UserLoginRequest request,
        [FromServices]UserLoginUseCase useCase)
    {
        var response = await useCase.Execute(request);
        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpPut]
    public async Task<ActionResult<Response>> UpdateCurrentUser(
        [FromBody]UpdateCurrentUserRequest request,
        [FromServices]UpdateCurrentUserUseCase useCase)
    {
        var response = await useCase.Execute(currentPersonId: User.GetPersonId(), request);
        return response.Success ? Ok(response) : BadRequest(response);
    }

    [Route("password")]
    [HttpPut]
    public async Task<ActionResult<Response>> ChangePassword(
        [FromBody]ChangePasswordRequest request,
        [FromServices]ChangePasswordUseCase useCase)
    {
        var response = await useCase.Execute(User.GetUserId(), request);
        return response.Success ? Ok(response) : BadRequest(response);
    }

    [AllowAnonymous]
    [Route("email-verification")]
    [HttpPost]
    public async Task<ActionResult<Response<UserLoginResponse>>> VerifyEmail(
        [FromBody]VerifyEmailRequest request,
        [FromServices]VerifyEmailUseCase useCase)
    {
        var response = await useCase.Execute(request);
        return response.Success ? Ok(response) : Unauthorized(response);
    }

    [AllowAnonymous]
    [Route("password-reset")]
    [HttpPost]
    public async Task<ActionResult<Response>> ResetForgottenPassword(
        [FromBody]ResetForgottenPasswordRequest request,
        [FromServices]ResetForgottenPasswordUseCase useCase)
    {
        var response = await useCase.Execute(request);
        return response.Success ? Ok(response) : BadRequest(response);
    }

    [AllowAnonymous]
    [Route("password-reset/send")]
    [HttpPost]
    public async Task<ActionResult<Response>> SendPasswordResetEmail(
        [FromBody]SendPasswordResetEmailRequest request,
        [FromServices]SendPasswordResetEmailUseCase useCase)
    {
        var response = await useCase.Execute(request);
        return response.Success ? Ok(response) : BadRequest(response);
    }
}
