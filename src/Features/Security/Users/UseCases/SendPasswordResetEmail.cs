namespace DentallApp.Features.Security.Users.UseCases;

public class SendPasswordResetEmailRequest
{
    public string Email { get; init; }
}

public class SendPasswordResetEmailUseCase
{
    private readonly DbContext _context;
    private readonly ITokenService _tokenService;
    private readonly IEmailService _emailService;

    public SendPasswordResetEmailUseCase(
        DbContext context, 
        ITokenService tokenService, 
        IEmailService emailService)
    {
        _context = context;
        _tokenService = tokenService;
        _emailService = emailService;
    }

    public async Task<Result> ExecuteAsync(SendPasswordResetEmailRequest request)
    {
        var user = await _context.Set<User>()
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

        var passwordResetToken = _tokenService.CreatePasswordResetToken(user.UserId, user.UserName, user.Password);
        await _emailService.SendEmailForResetPasswordAsync(user.UserName, user.Name, passwordResetToken);
        return Result.Success(SendPasswordResetLinkMessage);
    }
}
