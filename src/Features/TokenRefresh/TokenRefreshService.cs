namespace DentallApp.Features.TokenRefresh;

public class TokenRefreshService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private readonly IDateTimeProvider _dateTimeProvider;

    public TokenRefreshService(IUserRepository userRepository, ITokenService tokenService, IDateTimeProvider dateTimeProvider)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Response<TokenDto>> RefreshTokenAsync(TokenDto tokenDto)
    {
        var claimPrincipal = _tokenService.GetPrincipalFromExpiredAccessToken(tokenDto.AccessToken);
        if (claimPrincipal is null)
            return new Response<TokenDto>(AccessTokenInvalidMessage);

        var user = await _userRepository.GetByIdAsync(claimPrincipal.GetUserId());
        if (user is null)
            return new Response<TokenDto>(UsernameNotFoundMessage);

        if (user.RefreshToken != tokenDto.RefreshToken)
            return new Response<TokenDto>(RefreshTokenInvalidMessage);

        if (_dateTimeProvider.Now >= user.RefreshTokenExpiry)
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

    public async Task<Response> RevokeTokenAsync(int userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user is null)
            return new Response(UsernameNotFoundMessage);

        if (user.RefreshToken is null)
            return new Response(HasNoRefreshTokenMessage);

        user.RefreshToken = null;
        user.RefreshTokenExpiry = null;
        await _userRepository.SaveAsync();
        return new Response
        {
            Success = true,
            Message = RevokeTokenMessage
        };
    }
}
