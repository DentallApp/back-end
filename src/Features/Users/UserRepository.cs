namespace DentallApp.Features.Users;

public class UserRepository : Repository<User>, IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<User> GetFullUserProfile(string username)
        => await _context.Set<User>()
                         .Include(user => user.Person)
                            .ThenInclude(person => person.Gender)
                         .Include(user => user.UserRoles)
                            .ThenInclude(user => user.Role)
                         .Where(user => user.UserName == username)
                         .FirstOrDefaultAsync();

    public async Task<User> GetUserByName(string username)
        => await _context.Set<User>()
                         .Where(user => user.UserName == username)
                         .FirstOrDefaultAsync();

    public async Task<bool> UserExists(string username)
    {
        var user = await GetUserByName(username);
        return user is not null;
    }
}
