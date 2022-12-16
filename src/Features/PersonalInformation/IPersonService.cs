namespace DentallApp.Features.PersonalInformation;

public interface IPersonService
{
    Task<Response> CreatePersonAsync(PersonInsertDto personInsertDto);
}
