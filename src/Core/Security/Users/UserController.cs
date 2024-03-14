using DentallApp.Core.Security.Users.UseCases;

namespace DentallApp.Core.Security.Users;

[Authorize]
[Route("user")]
[ApiController]
public class UserController
{
    /// <summary>
    /// Creates a new basic user.
    /// </summary>
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType<Result>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<Result>(StatusCodes.Status409Conflict)]
    [AllowAnonymous]
    [HttpPost]
    public async Task<Result> Create(
        [FromBody]CreateBasicUserRequest request,
        CreateBasicUserUseCase useCase)
        => await useCase.ExecuteAsync(request);

    /// <summary>
    /// Allows the basic user or employee to authenticate.
    /// </summary>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<Result>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<Result>(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType<Result>(StatusCodes.Status403Forbidden)]
    [AllowAnonymous]
    [Route("login")]
    [HttpPost]
    public async Task<Result<UserLoginResponse>> Login(
        [FromBody]UserLoginRequest request,
        UserLoginUseCase useCase)
        => await useCase.ExecuteAsync(request);

    /// <summary>
    /// Updates the information of the currently logged in user.
    /// </summary>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<Result>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<Result>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<Result>(StatusCodes.Status403Forbidden)]
    [HttpPut]
    public async Task<Result> UpdateCurrentUser(
        [FromBody]UpdateCurrentUserRequest request,
        UpdateCurrentUserUseCase useCase)
        => await useCase.ExecuteAsync(request);

    /// <summary>
    /// Changes the password of the user who is currently logged in.
    /// </summary>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<Result>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<Result>(StatusCodes.Status404NotFound)]
    [Route("password")]
    [HttpPut]
    public async Task<Result> ChangePassword(
        [FromBody]ChangePasswordRequest request,
        ChangePasswordUseCase useCase)
        => await useCase.ExecuteAsync(request);

    /// <summary>
    /// Verifies a user's e-mail address.
    /// </summary>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<Result>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<Result>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<Result>(StatusCodes.Status409Conflict)]
    [AllowAnonymous]
    [Route("email-verification")]
    [HttpPost]
    public async Task<Result<UserLoginResponse>> VerifyEmail(
        [FromBody]VerifyEmailRequest request,
        VerifyEmailUseCase useCase)
        => await useCase.ExecuteAsync(request);

    /// <summary>
    /// Resets the user's password in case of forgetting it.
    /// </summary>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<Result>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<Result>(StatusCodes.Status404NotFound)]
    [AllowAnonymous]
    [Route("password-reset")]
    [HttpPost]
    public async Task<Result> ResetForgottenPassword(
        [FromBody]ResetForgottenPasswordRequest request,
        ResetForgottenPasswordUseCase useCase)
        => await useCase.ExecuteAsync(request);

    /// <summary>
    /// Sends an email to the user to reset their password.
    /// </summary>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<Result>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<Result>(StatusCodes.Status404NotFound)]
    [AllowAnonymous]
    [Route("password-reset/send")]
    [HttpPost]
    public async Task<Result> SendPasswordResetEmail(
        [FromBody]SendPasswordResetEmailRequest request,
        SendPasswordResetEmailUseCase useCase)
        => await useCase.ExecuteAsync(request);
}
