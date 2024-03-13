using DentallApp.Core.Persons.UseCases;

namespace DentallApp.Core.Persons;

[AuthorizeByRole(RoleName.Secretary)]
[Route("person")]
[ApiController]
public class PersonController
{
    /// <summary>
    /// Creates a person's information.
    /// </summary>
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType<Result>(StatusCodes.Status400BadRequest)]
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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet("search")]
	public async Task<IEnumerable<GetPersonsResponse>> GetAll(
        [FromQuery]string value,
        GetPersonsUseCase useCase)
        => await useCase.ExecuteAsync(value);
}
