namespace DentallApp.Features.Security.Users.UseCases;

public class ResetForgottenPasswordRequest
{
    public string Token { get; init; }
    public string NewPassword { get; init; }
}

public class ResetForgottenPasswordUseCase(
    DbContext context,
    ITokenService tokenService,
    IPasswordHasher passwordHasher)
{
    public async Task<Result> ExecuteAsync(ResetForgottenPasswordRequest request)
    {
        var claimIdentity = tokenService.GetClaimsIdentity(request.Token);
        if (claimIdentity is null)
            return Result.Invalid(Messages.PasswordResetTokenInvalid);

        if (!claimIdentity.HasClaim(CustomClaimsType.UserId))
            return Result.Invalid(new MissingClaimError(CustomClaimsType.UserId).Message);

        var userId = claimIdentity.GetUserId();
        var user = await context.Set<User>()
            .Where(user => user.Id == userId)
            .FirstOrDefaultAsync();

        if (user is null)
            return Result.NotFound(Messages.UsernameNotFound);

        var claimPrincipal = tokenService.ValidatePasswordResetToken(request.Token, user.Password);
        if (claimPrincipal is null)
            return Result.Invalid(Messages.PasswordResetTokenInvalid);

        user.Password = passwordHasher.HashPassword(request.NewPassword);
        await context.SaveChangesAsync();
        return Result.Success(Messages.PasswordSuccessfullyReset);
    }
}
