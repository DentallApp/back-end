namespace DentallApp.Features.Security.Users.UseCases;

public class VerifyEmailRequest
{
    public string Token { get; init; }
}

public class VerifyEmailUseCase(
    DbContext context,
    IUserRepository userRepository,
    ITokenService tokenService)
{
    public async Task<Result<UserLoginResponse>> ExecuteAsync(VerifyEmailRequest request)
    {
        var claimPrincipal = tokenService.ValidateEmailVerificationToken(request.Token);
        if (claimPrincipal is null)
            return Result.Invalid(Messages.EmailVerificationTokenInvalid);

        var user = await userRepository.GetFullUserProfileAsync(claimPrincipal.GetUserName());
        if (user is null)
            return Result.NotFound(Messages.UsernameNotFound);

        if (user.IsVerified())
            return Result.Conflict(Messages.AccountAlreadyVerified);

        var userLoginResponse   =  user.MapToUserLoginResponse();
        user.RefreshToken       = tokenService.CreateRefreshToken();
        user.RefreshTokenExpiry = tokenService.CreateExpiryForRefreshToken();
        var userRole = user.UserRoles.First();
        userRole.RoleId = (int)Role.Predefined.BasicUser;
        await context.SaveChangesAsync();

        userLoginResponse.Roles        = new[] { RoleName.BasicUser };
        userLoginResponse.AccessToken  = tokenService.CreateAccessToken(userLoginResponse.MapToUserClaims());
        userLoginResponse.RefreshToken = user.RefreshToken;
        return Result.Success(userLoginResponse, Messages.EmailSuccessfullyVerified);
    }
}
