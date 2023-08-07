namespace DentallApp.Features.Security.Users.UseCases;

public class ResetForgottenPasswordRequest
{
    public string Token { get; init; }
    public string NewPassword { get; init; }
}

public class ResetForgottenPasswordUseCase
{
    private readonly AppDbContext _context;
    private readonly ITokenService _tokenService;
    private readonly IPasswordHasher _passwordHasher;

    public ResetForgottenPasswordUseCase(
        AppDbContext context, 
        ITokenService tokenService, 
        IPasswordHasher passwordHasher)
    {
        _context = context;
        _tokenService = tokenService;
        _passwordHasher = passwordHasher;
    }

    public async Task<Response> Execute(ResetForgottenPasswordRequest request)
    {
        var claimIdentity = _tokenService.GetClaimsIdentity(request.Token);
        if (claimIdentity is null)
            return new Response(PasswordResetTokenInvalidMessage);

        if (!claimIdentity.HasClaim(CustomClaimsType.UserId))
            return new Response(string.Format(MissingClaimMessage, CustomClaimsType.UserId));

        var userId = claimIdentity.GetUserId();
        var user = await _context.Set<User>()
            .Where(user => user.Id == userId)
            .FirstOrDefaultAsync();

        if (user is null)
            return new Response(UsernameNotFoundMessage);

        var claimPrincipal = _tokenService.ValidatePasswordResetToken(request.Token, user.Password);
        if (claimPrincipal is null)
            return new Response(PasswordResetTokenInvalidMessage);

        user.Password = _passwordHasher.HashPassword(request.NewPassword);
        await _context.SaveChangesAsync();

        return new Response
        {
            Success = true,
            Message = PasswordSuccessfullyResetMessage
        };
    }
}
