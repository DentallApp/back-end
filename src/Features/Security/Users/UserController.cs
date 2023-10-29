using DentallApp.Features.Security.Users.UseCases;

namespace DentallApp.Features.Security.Users;

[Authorize]
[Route("user")]
[ApiController]
public class UserController : ControllerBase
{
    [AllowAnonymous]
    [HttpPost]
    public async Task<Result> Create(
        [FromBody]CreateBasicUserRequest request,
        [FromServices]CreateBasicUserUseCase useCase)
    {
        return await useCase.ExecuteAsync(request);
    }

    [AllowAnonymous]
    [Route("login")]
    [HttpPost]
    public async Task<Result<UserLoginResponse>> Login(
        [FromBody]UserLoginRequest request,
        [FromServices]UserLoginUseCase useCase)
    {
        return await useCase.ExecuteAsync(request);
    }

    [HttpPut]
    public async Task<Result> UpdateCurrentUser(
        [FromBody]UpdateCurrentUserRequest request,
        [FromServices]UpdateCurrentUserUseCase useCase)
    {
        return await useCase.ExecuteAsync(currentPersonId: User.GetPersonId(), request);
    }

    [Route("password")]
    [HttpPut]
    public async Task<Result> ChangePassword(
        [FromBody]ChangePasswordRequest request,
        [FromServices]ChangePasswordUseCase useCase)
    {
        return await useCase.ExecuteAsync(User.GetUserId(), request);
    }

    [AllowAnonymous]
    [Route("email-verification")]
    [HttpPost]
    public async Task<Result<UserLoginResponse>> VerifyEmail(
        [FromBody]VerifyEmailRequest request,
        [FromServices]VerifyEmailUseCase useCase)
    {
        return await useCase.ExecuteAsync(request);
    }

    [AllowAnonymous]
    [Route("password-reset")]
    [HttpPost]
    public async Task<Result> ResetForgottenPassword(
        [FromBody]ResetForgottenPasswordRequest request,
        [FromServices]ResetForgottenPasswordUseCase useCase)
    {
        return await useCase.ExecuteAsync(request);
    }

    [AllowAnonymous]
    [Route("password-reset/send")]
    [HttpPost]
    public async Task<Result> SendPasswordResetEmail(
        [FromBody]SendPasswordResetEmailRequest request,
        [FromServices]SendPasswordResetEmailUseCase useCase)
    {
        return await useCase.ExecuteAsync(request);
    }
}
