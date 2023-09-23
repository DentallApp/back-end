namespace DentallApp.Features.Security.RefreshToken.UseCases;

public class RevokeRefreshTokenUseCase
{
    private readonly AppDbContext _context;

    public RevokeRefreshTokenUseCase(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Response> ExecuteAsync(int userId)
    {
        var user = await _context.Set<User>()
            .Where(user => user.Id == userId)
            .FirstOrDefaultAsync();

        if (user is null)
            return new Response(UsernameNotFoundMessage);

        if (user.RefreshToken is null)
            return new Response(HasNoRefreshTokenMessage);

        user.RefreshToken = null;
        user.RefreshTokenExpiry = null;
        await _context.SaveChangesAsync();

        return new Response
        {
            Success = true,
            Message = RevokeTokenMessage
        };
    }
}
