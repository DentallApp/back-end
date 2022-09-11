namespace DentallApp.Features.Persons;

public interface IPersonService
{
    Task<Response> CreatePersonAsync(PersonInsertDto personInsertDto);
    Task<IEnumerable<PersonGetDto>> GetPersonsAsync(string valueToSearch);
}
