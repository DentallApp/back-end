﻿namespace DentallApp.Features.Users;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User> GetFullUserProfile(string userName)
    { 
        var user = await _context.Set<User>()
            .Include(user => user.Person)
               .ThenInclude(person => person.Gender)
            .Include(user => user.UserRoles)
               .ThenInclude(user => user.Role)
            .Where(user => user.UserName == userName)
            .FirstOrDefaultAsync();

        return user;
    }

    public async Task<bool> UserExists(string userName)
    {
        var user = await _context.Set<User>()
            .Where(user => user.UserName == userName)
            .Select(user => new { user.Id })
            .AsNoTracking()
            .FirstOrDefaultAsync();

        return user is not null;
    }
}
