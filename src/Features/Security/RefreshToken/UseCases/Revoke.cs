﻿namespace DentallApp.Features.Security.RefreshToken.UseCases;

public class RevokeRefreshTokenUseCase(DbContext context)
{
    public async Task<Result> ExecuteAsync(int userId)
    {
        var user = await context.Set<User>()
            .Where(user => user.Id == userId)
            .FirstOrDefaultAsync();

        if (user is null)
            return Result.NotFound(UsernameNotFoundMessage);

        if (user.RefreshToken is null)
            return Result.Failure(HasNoRefreshTokenMessage);

        user.RefreshToken = null;
        user.RefreshTokenExpiry = null;
        await context.SaveChangesAsync();
        return Result.Success(RevokeTokenMessage);
    }
}
