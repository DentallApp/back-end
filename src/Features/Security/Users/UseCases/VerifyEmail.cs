namespace DentallApp.Features.Security.Users.UseCases;

public class VerifyEmailRequest
{
    public string Token { get; init; }
}

public class VerifyEmailUseCase
{
    private readonly DbContext _context;
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;

    public VerifyEmailUseCase(
        DbContext context,
        IUserRepository userRepository,
        ITokenService tokenService)
    {
        _context = context;
        _userRepository = userRepository;
        _tokenService = tokenService;
    }

    public async Task<Result<UserLoginResponse>> ExecuteAsync(VerifyEmailRequest request)
    {
        var claimPrincipal = _tokenService.ValidateEmailVerificationToken(request.Token);
        if (claimPrincipal is null)
            return Result.Invalid(EmailVerificationTokenInvalidMessage);

        var user = await _userRepository.GetFullUserProfileAsync(claimPrincipal.GetUserName());
        if (user is null)
            return Result.NotFound(UsernameNotFoundMessage);

        if (user.IsVerified())
            return Result.Conflict(AccountAlreadyVerifiedMessage);

        var userLoginResponse   =  user.MapToUserLoginResponse();
        user.RefreshToken       = _tokenService.CreateRefreshToken();
        user.RefreshTokenExpiry = _tokenService.CreateExpiryForRefreshToken();
        var userRole = user.UserRoles.First();
        userRole.RoleId = RolesId.BasicUser;
        await _context.SaveChangesAsync();

        userLoginResponse.Roles        = new[] { RolesName.BasicUser };
        userLoginResponse.AccessToken  = _tokenService.CreateAccessToken(userLoginResponse.MapToUserClaims());
        userLoginResponse.RefreshToken = user.RefreshToken;
        return Result.Success(userLoginResponse, EmailSuccessfullyVerifiedMessage);
    }
}
