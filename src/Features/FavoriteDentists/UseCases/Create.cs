namespace DentallApp.Features.FavoriteDentists.UseCases;

public class CreateFavoriteDentistRequest
{
    public int DentistId { get; init; }
}

public class CreateFavoriteDentistUseCase(DbContext context)
{
    public async Task<Result<CreatedId>> ExecuteAsync(int userId, CreateFavoriteDentistRequest request)
    {
        var favoriteDentist = new FavoriteDentist
        {
            UserId    = userId,
            DentistId = request.DentistId
        };
        context.Add(favoriteDentist);
        await context.SaveChangesAsync();
        return Result.CreatedResource(favoriteDentist.Id);
    }
}
