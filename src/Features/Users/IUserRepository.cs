namespace DentallApp.Features.Users;

public interface IUserRepository : IRepository<User>
{
    Task<User> GetFullUserProfile(string username);
    Task<User> GetUserByName(string username);
    Task<bool> UserExists(string username);
    Task<UserResetPasswordDto> GetUserForResetPassword(string username);
}
