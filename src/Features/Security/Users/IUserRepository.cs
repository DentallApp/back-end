namespace DentallApp.Features.Users;

public interface IUserRepository
{
    Task<User> GetFullUserProfile(string userName);
    Task<bool> UserExists(string userName);
}
