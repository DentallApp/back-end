using DentallApp.Features.Persons.UseCases;

namespace DentallApp.Features.Persons;

[AuthorizeByRole(RolesName.Secretary)]
[Route("person")]
[ApiController]
public class PersonController
{
    /// <summary>
    /// Create a person's information.
    /// </summary>
    [HttpPost]
	public async Task<Result> Create(
        [FromBody]CreatePersonRequest request,
        CreatePersonUseCase useCase)
        => await useCase.ExecuteAsync(request);

    /// <summary>
    /// Gets a set of person information based on a search criteria.
    /// </summary>
    /// <param name="value">
    /// The value to search for (can be the name, last name or document).
    /// </param>
    /// <param name="useCase"></param>
    [HttpGet("search")]
	public async Task<IEnumerable<GetPersonsResponse>> GetAll(
        [FromQuery]string value,
        GetPersonsUseCase useCase)
        => await useCase.ExecuteAsync(value);
}