namespace DentallApp.Features.Security.Users.UseCases;

public class SendPasswordResetEmailRequest
{
    public string Email { get; init; }
}

public class SendPasswordResetEmailUseCase(
    DbContext context,
    ITokenService tokenService,
    IEmailService emailService)
{
    public async Task<Result> ExecuteAsync(SendPasswordResetEmailRequest request)
    {
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
            return Result.NotFound(UsernameNotFoundMessage);

        var passwordResetToken = tokenService.CreatePasswordResetToken(user.UserId, user.UserName, user.Password);
        await emailService.SendEmailForResetPasswordAsync(user.UserName, user.Name, passwordResetToken);
        return Result.Success(SendPasswordResetLinkMessage);
    }
}
