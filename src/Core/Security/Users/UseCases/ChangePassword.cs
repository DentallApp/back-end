namespace DentallApp.Core.Security.Users.UseCases;

public class ChangePasswordRequest
{
    public string OldPassword { get; init; }
    public string NewPassword { get; init; }
}

public class ChangePasswordValidator : AbstractValidator<ChangePasswordRequest>
{
    public ChangePasswordValidator()
    {
        RuleFor(request => request.OldPassword).NotEmpty();
        RuleFor(request => request.NewPassword).MustBeSecurePassword();
    }
}

public class ChangePasswordUseCase(
    DbContext context, 
    IPasswordHasher passwordHasher,
    ICurrentUser currentUser,
    ChangePasswordValidator validator)
{
    public async Task<Result> ExecuteAsync(ChangePasswordRequest request)
    {
        var result = validator.Validate(request);
        if (result.IsFailed())
            return result.Invalid();

        var user = await context.Set<User>()
            .Where(user => user.Id == currentUser.UserId)
            .FirstOrDefaultAsync();

        if (user is null)
            return Result.NotFound(Messages.UsernameNotFound);

        if (!passwordHasher.Verify(request.OldPassword, user.Password))
            return Result.Invalid(Messages.OldPasswordIncorrect);

        user.Password = passwordHasher.HashPassword(request.NewPassword);
        await context.SaveChangesAsync();
        return Result.Success(Messages.PasswordSuccessfullyReset);
    }
}
