namespace DentallApp.Core.Security.RefreshToken.UseCases;

public class CreateRefreshTokenRequest
{
    public string OldAccessToken { get; init; }
    public string OldRefreshToken { get; init; }
}

public class CreateRefreshTokenResponse
{
    public string NewAccessToken { get; init; }
    public string NewRefreshToken { get; init; }
}

public class CreateRefreshTokenUseCase(
    DbContext context,
    ITokenService tokenService,
    IDateTimeService dateTimeService)
{
    public async Task<Result<CreateRefreshTokenResponse>> ExecuteAsync(CreateRefreshTokenRequest request)
    {
        var claimPrincipal = tokenService.GetPrincipalFromExpiredAccessToken(request.OldAccessToken);
        if (claimPrincipal is null)
            return Result.Unauthorized(Messages.AccessTokenInvalid);

        int userId = claimPrincipal.GetUserId();
        var user = await context.Set<User>()
            .Where(user => user.Id == userId)
            .FirstOrDefaultAsync();

        if (user is null)
            return Result.NotFound(Messages.UsernameNotFound);

        if (user.RefreshToken != request.OldRefreshToken)
            return Result.Unauthorized(Messages.RefreshTokenInvalid);

        if (dateTimeService.Now >= user.RefreshTokenExpiry)
            return Result.Unauthorized(Messages.RefreshTokenExpired);

        var response = new CreateRefreshTokenResponse
        {
            NewAccessToken  = tokenService.CreateAccessToken(claimPrincipal.Claims),
            NewRefreshToken = tokenService.CreateRefreshToken()
        };
        user.RefreshToken = response.NewRefreshToken;
        await context.SaveChangesAsync();
        return Result.Success(response, Messages.UpdatedAccessToken);
    }
}
