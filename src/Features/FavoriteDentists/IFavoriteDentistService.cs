namespace DentallApp.Features.FavoriteDentists;

public interface IFavoriteDentistService
{
    Task<Response> CreateFavoriteDentistAsync(int userId, FavoriteDentistInsertDto favoriteDentistInsertDto);
    Task<Response> RemoveByFavoriteDentistIdAsync(int userId, int favoriteDentistId);
    Task<Response> RemoveByUserIdAndDentistIdAsync(int userId, int dentistId);
}
