namespace DentallApp.Core.Security.RefreshToken.UseCases;

public class RevokeRefreshTokenUseCase(DbContext context, ICurrentUser currentUser)
{
    public async Task<Result> ExecuteAsync()
    {
        var user = await context.Set<User>()
            .Where(user => user.Id == currentUser.UserId)
            .FirstOrDefaultAsync();

        if (user is null)
            return Result.NotFound(Messages.UsernameNotFound);

        if (user.RefreshToken is null)
            return Result.Failure(Messages.HasNoRefreshToken);

        user.RefreshToken = null;
        user.RefreshTokenExpiry = null;
        await context.SaveChangesAsync();
        return Result.Success(Messages.RevokeToken);
    }
}
