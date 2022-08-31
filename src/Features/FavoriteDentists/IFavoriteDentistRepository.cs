namespace DentallApp.Features.FavoriteDentists;

public interface IFavoriteDentistRepository : IRepository<FavoriteDentist>
{
    Task<FavoriteDentist> GetByUserIdAndDentistIdAsync(int userId, int dentistId);
    Task<IEnumerable<FavoriteDentistGetDto>> GetFavoriteDentistsAsync(int userId);
    Task<IEnumerable<DentistGetDto>> GetListOfDentistsAsync(int userId);
}
