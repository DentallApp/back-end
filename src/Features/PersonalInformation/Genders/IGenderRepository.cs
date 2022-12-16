namespace DentallApp.Features.PersonalInformation.Genders;

public interface IGenderRepository : IRepository<Gender>
{
    Task<IEnumerable<GenderGetDto>> GetGendersAsync();
}
