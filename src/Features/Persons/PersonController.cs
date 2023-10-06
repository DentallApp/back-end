using DentallApp.Features.Persons.UseCases;

namespace DentallApp.Features.Persons;

[AuthorizeByRole(RolesName.Secretary)]
[Route("person")]
[ApiController]
public class PersonController : ControllerBase
{
    /// <summary>
    /// Create a person's information.
    /// </summary>
    [HttpPost]
	public async Task<ActionResult<Response>> Create(
        [FromBody]CreatePersonRequest request,
        [FromServices]CreatePersonUseCase useCase)
	{
		var response = await useCase.ExecuteAsync(request);
		return response.Success ? CreatedAtAction(nameof(Create), response) : BadRequest(response);
	}

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
        [FromServices]GetPersonsUseCase useCase)
    {
        return await useCase.ExecuteAsync(value);
    }
}