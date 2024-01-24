namespace DentallApp.Features.Security.Users.UseCases;

public class ChangePasswordRequest
{
    public string OldPassword { get; init; }
    public string NewPassword { get; init; }
}

public class ChangePasswordUseCase(DbContext context, IPasswordHasher passwordHasher)
{
    public async Task<Result> ExecuteAsync(int userId, ChangePasswordRequest request)
    {
        var user = await context.Set<User>()
            .Where(user => user.Id == userId)
            .FirstOrDefaultAsync();

        if (user is null)
            return Result.NotFound(UsernameNotFoundMessage);

        if (!passwordHasher.Verify(request.OldPassword, user.Password))
            return Result.Invalid(OldPasswordIncorrectMessage);

        user.Password = passwordHasher.HashPassword(request.NewPassword);
        await context.SaveChangesAsync();
        return Result.Success(PasswordSuccessfullyResetMessage);
    }
}
