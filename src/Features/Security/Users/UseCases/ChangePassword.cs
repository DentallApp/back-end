namespace DentallApp.Features.Security.Users.UseCases;

public class ChangePasswordRequest
{
    public string OldPassword { get; init; }
    public string NewPassword { get; init; }
}

public class ChangePasswordUseCase
{
    private readonly DbContext _context;
    private readonly IPasswordHasher _passwordHasher;

    public ChangePasswordUseCase(DbContext context, IPasswordHasher passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }

    public async Task<Result> ExecuteAsync(int userId, ChangePasswordRequest request)
    {
        var user = await _context.Set<User>()
            .Where(user => user.Id == userId)
            .FirstOrDefaultAsync();

        if (user is null)
            return Result.NotFound(UsernameNotFoundMessage);

        if (!_passwordHasher.Verify(request.OldPassword, user.Password))
            return Result.Invalid(OldPasswordIncorrectMessage);

        user.Password = _passwordHasher.HashPassword(request.NewPassword);
        await _context.SaveChangesAsync();
        return Result.Success(PasswordSuccessfullyResetMessage);
    }
}
