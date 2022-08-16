namespace DentallApp.Features.Authentication;

public interface IAuthService
{
    Task<Response> LoginAsync(string username, string password);
}
