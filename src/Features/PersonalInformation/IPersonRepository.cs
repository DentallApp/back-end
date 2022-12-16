namespace DentallApp.Features.PersonalInformation;

public interface IPersonRepository : IRepository<Person>
{
    Task<IEnumerable<PersonGetDto>> GetPersonsAsync(string valueToSearch);
}
