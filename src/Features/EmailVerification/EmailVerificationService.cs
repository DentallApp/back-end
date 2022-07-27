namespace DentallApp.Features.EmailVerification;

public class EmailVerificationService : IEmailVerificationService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;

    public EmailVerificationService(IUserRepository userRepository, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
    }

    public async Task<Response<UserLoginDto>> VerifyEmailAsync(string token)
    {
        var claimPrincipal = _tokenService.ValidateEmailVerificationToken(token);
        if (claimPrincipal is null)
            return new Response<UserLoginDto>(EmailVerificationTokenInvalidMessage);

        var user = await _userRepository.GetFullUserProfileAsync(claimPrincipal.GetUserName());
        if (user is null)
            return new Response<UserLoginDto>(UsernameNotFoundMessage);

        if (user.IsVerified())
            return new Response<UserLoginDto>(AccountAlreadyVerifiedMessage);

        var userLoginDto = user.MapToUserLoginDto();
        user.RefreshToken = _tokenService.CreateRefreshToken();
        user.RefreshTokenExpiry = _tokenService.CreateExpiryForRefreshToken();
        var userRole = user.UserRoles.First();
        userRole.RoleId = RolesId.BasicUser;
        await _userRepository.SaveAsync();

        userLoginDto.Roles = new[] { RolesName.BasicUser };
        userLoginDto.AccessToken = _tokenService.CreateAccessToken(userLoginDto.MapToUserClaims());
        userLoginDto.RefreshToken = user.RefreshToken;
        return new Response<UserLoginDto>()
        {
            Success = true,
            Data = userLoginDto,
            Message = EmailSuccessfullyVerifiedMessage
        };
    }
}
