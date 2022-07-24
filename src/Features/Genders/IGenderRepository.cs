namespace DentallApp.Features.Genders;

public interface IGenderRepository : IRepository<Gender>
{
    Task<IEnumerable<GenderGetDto>> GetGenders();
}
