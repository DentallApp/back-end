namespace DentallApp.Features.Persons.Genders;

public interface IGenderRepository : IRepository<Gender>
{
    Task<IEnumerable<GenderGetDto>> GetGendersAsync();
}
