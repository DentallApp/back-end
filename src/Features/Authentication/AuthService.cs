namespace DentallApp.Features.Authentication;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private readonly IPasswordHasher _passwordHasher;

    public AuthService(IUserRepository userRepository, 
                       ITokenService tokenService,
                       IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
        _passwordHasher = passwordHasher;
    }

    public async Task<Response> LoginAsync(string username, string password)
    {
        var user = await _userRepository.GetFullUserProfile(username);
        if (user is null || !_passwordHasher.Verify(text: password, passwordHash: user.Password))
            return new Response(EmailOrPasswordIncorrectMessage);

        if (user.IsUnverified())
            return new Response(EmailNotConfirmedMessage);

        user.RefreshToken = _tokenService.CreateRefreshToken();
        user.RefreshTokenExpiry = _tokenService.CreateExpiryForRefreshToken();
        await _userRepository.SaveAsync();

        var userLoginDto = user.MapToUserLoginDto();
        userLoginDto.AccessToken = _tokenService.CreateAccessToken(userLoginDto.MapToUserClaims());
        userLoginDto.RefreshToken = user.RefreshToken;
        return new Response
        {
            Success = true,
            Data = userLoginDto,
            Message = SuccessfulLoginMessage
        };
    }

    public async Task<Response<TokenDto>> RefreshTokenAsync(TokenDto tokenDto)
    {
        var claimPrincipal = _tokenService.GetPrincipalFromExpiredAccessToken(tokenDto.AccessToken);
        if (claimPrincipal is null)
            return new Response<TokenDto>(AccessTokenInvalidMessage);

        var user = await _userRepository.GetByIdAsync(claimPrincipal.GetUserId());
        if (user is null)
            return new Response<TokenDto>(UsernameNotFoundMessage);
        var auth = claimPrincipal.Identity.IsAuthenticated;
        if (user.RefreshToken != tokenDto.RefreshToken)
            return new Response<TokenDto>(RefreshTokenInvalidMessage);

        if (DateTime.Now >= user.RefreshTokenExpiry)
            return new Response<TokenDto>(RefreshTokenExpiredMessage);

        tokenDto.AccessToken = _tokenService.CreateAccessToken(claimPrincipal.Claims);
        tokenDto.RefreshToken = _tokenService.CreateRefreshToken();
        user.RefreshToken = tokenDto.RefreshToken;
        await _userRepository.SaveAsync();
        return new Response<TokenDto>
        {
            Success = true,
            Data = tokenDto,
            Message = UpdatedAccessTokenMessage
        };
    }

    public async Task<Response> RevokeRefreshTokenAsync(int userid)
    {
        var user = await _userRepository.GetByIdAsync(userid);
        if (user is null)
            return new Response(UsernameNotFoundMessage);

        if (user.RefreshToken is null)
            return new Response(HasNoRefreshTokenMessage);

        user.RefreshToken = null;
        await _userRepository.SaveAsync();
        return new Response
        {
            Success = true,
            Message = RevokeTokenMessage
        };
    }
}
