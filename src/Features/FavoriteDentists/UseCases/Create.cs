namespace DentallApp.Features.FavoriteDentists.UseCases;

public class CreateFavoriteDentistRequest
{
    public int DentistId { get; init; }
}

public class CreateFavoriteDentistUseCase
{
    private readonly AppDbContext _context;

    public CreateFavoriteDentistUseCase(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Response<InsertedIdDto>> Execute(int userId, CreateFavoriteDentistRequest request)
    {
        var favoriteDentist = new FavoriteDentist
        {
            UserId    = userId,
            DentistId = request.DentistId
        };
        _context.Add(favoriteDentist);
        await _context.SaveChangesAsync();

        return new Response<InsertedIdDto>
        {
            Success = true,
            Data    = new InsertedIdDto { Id = favoriteDentist.Id },
            Message = CreateResourceMessage
        };
    }
}
