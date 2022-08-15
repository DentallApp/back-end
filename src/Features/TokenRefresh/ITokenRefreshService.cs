namespace DentallApp.Features.TokenRefresh;

public interface ITokenRefreshService
{
    Task<Response<TokenDto>> RefreshTokenAsync(TokenDto tokenDto);
    Task<Response> RevokeTokenAsync(int userId);
}
