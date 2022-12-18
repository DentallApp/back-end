﻿namespace DentallApp.Features.PersonalInformation;

[AuthorizeByRole(RolesName.Secretary)]
[Route("person")]
[ApiController]
public class PersonController : ControllerBase
{
	private readonly IPersonService _personService;
	private readonly IPersonRepository _personRepository;

	public PersonController(IPersonService personService, IPersonRepository personRepository)
	{
		_personService = personService;
		_personRepository = personRepository;
	}

	/// <summary>
	/// Registra la información de una persona.
	/// </summary>
	[HttpPost]
	public async Task<ActionResult<Response>> Post([FromBody]PersonInsertDto personInsertDto)
	{
		var response = await _personService.CreatePersonAsync(personInsertDto);
		if (response.Success)
			return CreatedAtAction(nameof(Post), response);

		return BadRequest(response);
	}

    /// <summary>
    /// Obtiene un conjunto de información de personas en base a un criterio de búsqueda.
    /// </summary>
    /// <param name="value">El valor a buscar (puede ser el nombre, apellido o cédula de una persona).</param>
    [HttpGet("search")]
	public async Task<IEnumerable<PersonGetDto>> Get([FromQuery]string value)
		=> await _personRepository.GetPersonsAsync(value);
}