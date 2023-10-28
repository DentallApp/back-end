namespace DentallApp.Features.Security.Users.UseCases;

public class ResetForgottenPasswordRequest
{
    public string Token { get; init; }
    public string NewPassword { get; init; }
}

public class ResetForgottenPasswordUseCase
{
    private readonly DbContext _context;
    private readonly ITokenService _tokenService;
    private readonly IPasswordHasher _passwordHasher;

    public ResetForgottenPasswordUseCase(
        DbContext context, 
        ITokenService tokenService, 
        IPasswordHasher passwordHasher)
    {
        _context = context;
        _tokenService = tokenService;
        _passwordHasher = passwordHasher;
    }

    public async Task<Result> ExecuteAsync(ResetForgottenPasswordRequest request)
    {
        var claimIdentity = _tokenService.GetClaimsIdentity(request.Token);
        if (claimIdentity is null)
            return Result.Invalid(PasswordResetTokenInvalidMessage);

        if (!claimIdentity.HasClaim(CustomClaimsType.UserId))
            return Result.Invalid(string.Format(MissingClaimMessage, CustomClaimsType.UserId));

        var userId = claimIdentity.GetUserId();
        var user = await _context.Set<User>()
            .Where(user => user.Id == userId)
            .FirstOrDefaultAsync();

        if (user is null)
            return Result.NotFound(UsernameNotFoundMessage);

        var claimPrincipal = _tokenService.ValidatePasswordResetToken(request.Token, user.Password);
        if (claimPrincipal is null)
            return Result.Invalid(PasswordResetTokenInvalidMessage);

        user.Password = _passwordHasher.HashPassword(request.NewPassword);
        await _context.SaveChangesAsync();
        return Result.Success(PasswordSuccessfullyResetMessage);
    }
}
