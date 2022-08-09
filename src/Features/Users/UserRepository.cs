namespace DentallApp.Features.Users;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context) { }

    public async Task<User> GetFullUserProfileAsync(string username)
        => await Context.Set<User>()
                        .Include(user => user.Person)
                           .ThenInclude(person => person.Gender)
                        .Include(user => user.UserRoles)
                           .ThenInclude(user => user.Role)
                        .Where(user => user.UserName == username)
                        .FirstOrDefaultAsync();

    public async Task<User> GetUserByNameAsync(string username)
        => await Context.Set<User>()
                        .Where(user => user.UserName == username)
                        .FirstOrDefaultAsync();

    public async Task<bool> UserExistsAsync(string username)
    {
        var user = await GetUserByNameAsync(username);
        return user is not null;
    }

    public async Task<UserResetPasswordDto> GetUserForResetPasswordAsync(string username)
        => await Context.Set<User>()
                        .Include(user => user.Person)
                        .Where(user => user.UserName == username)
                        .Select(user => user.MapToUserResetPasswordDto())
                        .FirstOrDefaultAsync();
}
