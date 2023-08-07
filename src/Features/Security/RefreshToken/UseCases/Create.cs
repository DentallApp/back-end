namespace DentallApp.Features.Security.RefreshToken.UseCases;

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

public class CreateRefreshTokenUseCase
{
    private readonly AppDbContext _context;
    private readonly ITokenService _tokenService;
    private readonly IDateTimeProvider _dateTimeProvider;

    public CreateRefreshTokenUseCase(
        AppDbContext context, 
        ITokenService tokenService, 
        IDateTimeProvider dateTimeProvider)
    {
        _context = context;
        _tokenService = tokenService;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Response<CreateRefreshTokenResponse>> Execute(CreateRefreshTokenRequest request)
    {
        var claimPrincipal = _tokenService.GetPrincipalFromExpiredAccessToken(request.OldAccessToken);
        if (claimPrincipal is null)
            return new Response<CreateRefreshTokenResponse>(AccessTokenInvalidMessage);

        int userId = claimPrincipal.GetUserId();
        var user = await _context.Set<User>()
            .Where(user => user.Id == userId)
            .FirstOrDefaultAsync();

        if (user is null)
            return new Response<CreateRefreshTokenResponse>(UsernameNotFoundMessage);

        if (user.RefreshToken != request.OldRefreshToken)
            return new Response<CreateRefreshTokenResponse>(RefreshTokenInvalidMessage);

        if (_dateTimeProvider.Now >= user.RefreshTokenExpiry)
            return new Response<CreateRefreshTokenResponse>(RefreshTokenExpiredMessage);

        var response = new CreateRefreshTokenResponse
        {
            NewAccessToken  = _tokenService.CreateAccessToken(claimPrincipal.Claims),
            NewRefreshToken = _tokenService.CreateRefreshToken()
        };
        user.RefreshToken = response.NewRefreshToken;
        await _context.SaveChangesAsync();

        return new Response<CreateRefreshTokenResponse>
        {
            Success = true,
            Data    = response,
            Message = UpdatedAccessTokenMessage
        };
    }
}
