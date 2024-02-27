namespace DentallApp.Core.Security.Users.UseCases;

public class SendPasswordResetEmailRequest
{
    public string Email { get; init; }
}

public class SendPasswordResetEmailValidator : AbstractValidator<SendPasswordResetEmailRequest>
{
    public SendPasswordResetEmailValidator()
    {
        RuleFor(request => request.Email)
            .NotEmpty()
            .EmailAddress();
    }
}

public class SendPasswordResetEmailUseCase(
    DbContext context,
    ITokenService tokenService,
    IEmailService emailService,
    SendPasswordResetEmailValidator validator)
{
    public async Task<Result> ExecuteAsync(SendPasswordResetEmailRequest request)
    {
        var result = validator.Validate(request);
        if (result.IsFailed())
            return result.Invalid();

        var user = await context.Set<User>()
            .Where(user => user.UserName == request.Email)
            .Select(user => new
            {
                UserId   = user.Id,
                UserName = user.UserName,
                Name     = user.Person.Names,
                Password = user.Password
            })
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (user is null)
            return Result.NotFound(Messages.UsernameNotFound);

        var passwordResetToken = tokenService.CreatePasswordResetToken(user.UserId, user.UserName, user.Password);
        await emailService.SendEmailForResetPasswordAsync(user.UserName, user.Name, passwordResetToken);
        return Result.Success(Messages.SendPasswordResetLink);
    }
}
