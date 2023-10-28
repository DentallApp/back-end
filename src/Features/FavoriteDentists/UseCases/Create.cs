namespace DentallApp.Features.FavoriteDentists.UseCases;

public class CreateFavoriteDentistRequest
{
    public int DentistId { get; init; }
}

public class CreateFavoriteDentistUseCase
{
    private readonly DbContext _context;

    public CreateFavoriteDentistUseCase(DbContext context)
    {
        _context = context;
    }

    public async Task<Result<CreatedId>> ExecuteAsync(int userId, CreateFavoriteDentistRequest request)
    {
        var favoriteDentist = new FavoriteDentist
        {
            UserId    = userId,
            DentistId = request.DentistId
        };
        _context.Add(favoriteDentist);
        await _context.SaveChangesAsync();
        return Result.CreatedResource(favoriteDentist.Id);
    }
}
