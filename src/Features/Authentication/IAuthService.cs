namespace DentallApp.Features.Authentication;

public interface IAuthService
{
    Task<Response> LoginAsync(string username, string password);
    Task<Response<TokenDto>> RefreshTokenAsync(TokenDto tokenDto);
    Task<Response> RevokeRefreshTokenAsync(int userid);
}
