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
            return Result.Invalid(EmailVerificationTokenInvalidMessage);

        var user = await userRepository.GetFullUserProfileAsync(claimPrincipal.GetUserName());
        if (user is null)
            return Result.NotFound(UsernameNotFoundMessage);

        if (user.IsVerified())
            return Result.Conflict(AccountAlreadyVerifiedMessage);

        var userLoginResponse   =  user.MapToUserLoginResponse();
        user.RefreshToken       = tokenService.CreateRefreshToken();
        user.RefreshTokenExpiry = tokenService.CreateExpiryForRefreshToken();
        var userRole = user.UserRoles.First();
        userRole.RoleId = RolesId.BasicUser;
        await context.SaveChangesAsync();

        userLoginResponse.Roles        = new[] { RolesName.BasicUser };
        userLoginResponse.AccessToken  = tokenService.CreateAccessToken(userLoginResponse.MapToUserClaims());
        userLoginResponse.RefreshToken = user.RefreshToken;
        return Result.Success(userLoginResponse, EmailSuccessfullyVerifiedMessage);
    }
}
