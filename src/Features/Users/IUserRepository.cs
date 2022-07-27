namespace DentallApp.Features.Users;

public interface IUserRepository : IRepository<User>
{
    Task<User> GetFullUserProfileAsync(string username);
    Task<User> GetUserByNameAsync(string username);
    Task<bool> UserExistsAsync(string username);
    Task<UserResetPasswordDto> GetUserForResetPasswordAsync(string username);
}
