namespace DentallApp.Features.FavoriteDentists.UseCases;

public class DeleteFavoriteDentistRequest
{
    public int UserId { get; init; }
    public int FavoriteDentistId { get; init; }
}

/// <summary>
/// Represents the use case to delete a favorite dentist of a basic user.
/// </summary>
public class DeleteFavoriteDentistUseCase
{
    private readonly DbContext _context;

    public DeleteFavoriteDentistUseCase(DbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Executes the use case. Deletes by favorite dentist id.
    /// </summary>
    /// <param name="request">Contains the user id and favorite dentist id.</param>
    public async Task<Result> ExecuteAsync(DeleteFavoriteDentistRequest request)
    {
        var favoriteDentist = await _context.Set<FavoriteDentist>()
            .Where(favoriteDentist => favoriteDentist.Id == request.FavoriteDentistId)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (favoriteDentist is null)
            return Result.NotFound();

        if (favoriteDentist.UserId != request.UserId)
            return Result.Forbidden(ResourceFromAnotherUserMessage);

        _context.Remove(favoriteDentist);
        await _context.SaveChangesAsync();
        return Result.DeletedResource();
    }

    /// <summary>
    /// Executes the use case. Deletes by user id and dentist id.
    /// </summary>
    /// <param name="userId">The basic user id.</param>
    /// <param name="dentistId">The dentist id.</param>
    public async Task<Result> ExecuteAsync(int userId, int dentistId)
    {
        int deletedRows = await _context.Set<FavoriteDentist>()
            .Where(favoriteDentist =>
                favoriteDentist.UserId == userId &&
                favoriteDentist.DentistId == dentistId)
            .ExecuteDeleteAsync();

        if (deletedRows == 0)
            return Result.NotFound();

        return Result.DeletedResource();
    }
}
