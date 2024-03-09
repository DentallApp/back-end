namespace DentallApp.Core.FavoriteDentists.UseCases;

public class FavoriteDentistId
{
    public int Value { get; init; }
}

/// <summary>
/// Represents the use case to delete a favorite dentist of a basic user.
/// </summary>
public class DeleteFavoriteDentistUseCase(
    DbContext context,
    ICurrentUser currentUser)
{
    /// <summary>
    /// Executes the use case. Deletes by favorite dentist id.
    /// </summary>
    /// <param name="id">Contains the favorite dentist id.</param>
    public async Task<Result> ExecuteAsync(FavoriteDentistId id)
    {
        var favoriteDentist = await context.Set<FavoriteDentist>()
            .Where(favoriteDentist => favoriteDentist.Id == id.Value)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (favoriteDentist is null)
            return Result.NotFound();

        if (favoriteDentist.UserId != currentUser.UserId)
            return Result.Forbidden(Messages.ResourceFromAnotherUser);

        context.Remove(favoriteDentist);
        await context.SaveChangesAsync();
        return Result.DeletedResource();
    }

    /// <summary>
    /// Executes the use case. Deletes by dentist id.
    /// </summary>
    /// <param name="dentistId">The dentist id.</param>
    public async Task<Result> ExecuteAsync(int dentistId)
    {
        int deletedRows = await context.Set<FavoriteDentist>()
            .Where(favoriteDentist =>
                favoriteDentist.UserId == currentUser.UserId &&
                favoriteDentist.DentistId == dentistId)
            .ExecuteDeleteAsync();

        if (deletedRows == 0)
            return Result.NotFound();

        return Result.DeletedResource();
    }
}
