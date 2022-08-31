namespace DentallApp.Features.FavoriteDentists;

public class FavoriteDentistService : IFavoriteDentistService
{
    private readonly IFavoriteDentistRepository _favoriteDentistRepository;

    public FavoriteDentistService(IFavoriteDentistRepository favoriteDentistRepository)
    {
        _favoriteDentistRepository = favoriteDentistRepository;
    }

    public async Task<Response> CreateFavoriteDentistAsync(int userId, FavoriteDentistInsertDto favoriteDentistInsertDto)
    {
        _favoriteDentistRepository.Insert(new FavoriteDentist 
        { 
            UserId = userId,
            DentistId = favoriteDentistInsertDto.DentistId 
        });
        await _favoriteDentistRepository.SaveAsync();

        return new Response
        {
            Success = true,
            Message = CreateResourceMessage
        };
    }

    public async Task<Response> RemoveByFavoriteDentistIdAsync(int userId, int favoriteDentistId)
    {
        var favoriteDentist = await _favoriteDentistRepository.GetByIdAsync(favoriteDentistId);
        if (favoriteDentist is null)
            return new Response(ResourceNotFoundMessage);

        if (favoriteDentist.UserId != userId)
            return new Response(ResourceFromAnotherUserMessage);

        _favoriteDentistRepository.Delete(favoriteDentist);
        await _favoriteDentistRepository.SaveAsync();

        return new Response
        {
            Success = true,
            Message = DeleteResourceMessage
        };
    }

    public async Task<Response> RemoveByUserIdAndDentistIdAsync(int userId, int dentistId)
    {
        var favoriteDentist = await _favoriteDentistRepository.GetByUserIdAndDentistIdAsync(userId, dentistId);
        if (favoriteDentist is null)
            return new Response(ResourceNotFoundMessage);

        _favoriteDentistRepository.Delete(favoriteDentist);
        await _favoriteDentistRepository.SaveAsync();

        return new Response
        {
            Success = true,
            Message = DeleteResourceMessage
        };
    }

    public async Task<IEnumerable<FavoriteDentistGetDto>> GetFavoriteDentistsAsync(int userId)
        => await _favoriteDentistRepository.GetFavoriteDentistsAsync(userId);

    public async Task<IEnumerable<DentistGetDto>> GetListOfDentistsAsync(int userId)
        => await _favoriteDentistRepository.GetListOfDentistsAsync(userId);
}
