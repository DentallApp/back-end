namespace DentallApp.Shared.Persistence.Repositories;

public interface IUserRepository
{
    Task<User> GetFullUserProfile(string userName);
    Task<bool> UserExists(string userName);
}
