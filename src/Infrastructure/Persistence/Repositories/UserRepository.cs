namespace DentallApp.Infrastructure.Persistence.Repositories;

public class UserRepository(DbContext context) : IUserRepository
{
    public async Task<User> GetFullUserProfileAsync(string userName)
    { 
        var user = await context.Set<User>()
            .Include(user => user.Person)
               .ThenInclude(person => person.Gender)
            .Include(user => user.UserRoles)
               .ThenInclude(user => user.Role)
            .Where(user => user.UserName == userName)
            .FirstOrDefaultAsync();

        return user;
    }

    public async Task<bool> UserExistsAsync(string userName)
    {
        var user = await context.Set<User>()
            .Where(user => user.UserName == userName)
            .Select(user => new { user.Id })
            .AsNoTracking()
            .FirstOrDefaultAsync();

        return user is not null;
    }
}
