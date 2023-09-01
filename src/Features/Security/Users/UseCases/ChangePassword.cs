namespace DentallApp.Features.Security.Users.UseCases;

public class ChangePasswordRequest
{
    public string OldPassword { get; init; }
    public string NewPassword { get; init; }
}

public class ChangePasswordUseCase
{
    private readonly AppDbContext _context;
    private readonly IPasswordHasher _passwordHasher;

    public ChangePasswordUseCase(AppDbContext context, IPasswordHasher passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }

    public async Task<Response> Execute(int userId, ChangePasswordRequest request)
    {
        var user = await _context.Set<User>()
            .Where(user => user.Id == userId)
            .FirstOrDefaultAsync();

        if (user is null)
            return new Response(UsernameNotFoundMessage);

        if (!_passwordHasher.Verify(request.OldPassword, user.Password))
            return new Response(OldPasswordIncorrectMessage);

        user.Password = _passwordHasher.HashPassword(request.NewPassword);
        await _context.SaveChangesAsync();

        return new Response
        {
            Success = true,
            Message = PasswordSuccessfullyResetMessage
        };
    }
}
