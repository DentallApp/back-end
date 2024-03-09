using DentallApp.Core.Security.Users.UseCases;

namespace DentallApp.Core.Security.Users;

[Authorize]
[Route("user")]
[ApiController]
public class UserController
{
    [AllowAnonymous]
    [HttpPost]
    public async Task<Result> Create(
        [FromBody]CreateBasicUserRequest request,
        CreateBasicUserUseCase useCase)
        => await useCase.ExecuteAsync(request);

    [AllowAnonymous]
    [Route("login")]
    [HttpPost]
    public async Task<Result<UserLoginResponse>> Login(
        [FromBody]UserLoginRequest request,
        UserLoginUseCase useCase)
        => await useCase.ExecuteAsync(request);

    [HttpPut]
    public async Task<Result> UpdateCurrentUser(
        [FromBody]UpdateCurrentUserRequest request,
        UpdateCurrentUserUseCase useCase)
        => await useCase.ExecuteAsync(request);

    [Route("password")]
    [HttpPut]
    public async Task<Result> ChangePassword(
        [FromBody]ChangePasswordRequest request,
        ChangePasswordUseCase useCase)
        => await useCase.ExecuteAsync(request);

    [AllowAnonymous]
    [Route("email-verification")]
    [HttpPost]
    public async Task<Result<UserLoginResponse>> VerifyEmail(
        [FromBody]VerifyEmailRequest request,
        VerifyEmailUseCase useCase)
        => await useCase.ExecuteAsync(request);

    [AllowAnonymous]
    [Route("password-reset")]
    [HttpPost]
    public async Task<Result> ResetForgottenPassword(
        [FromBody]ResetForgottenPasswordRequest request,
        ResetForgottenPasswordUseCase useCase)
        => await useCase.ExecuteAsync(request);

    [AllowAnonymous]
    [Route("password-reset/send")]
    [HttpPost]
    public async Task<Result> SendPasswordResetEmail(
        [FromBody]SendPasswordResetEmailRequest request,
        SendPasswordResetEmailUseCase useCase)
        => await useCase.ExecuteAsync(request);
}
