namespace DentallApp.Features.Offices;

[Route("office")]
[ApiController]
public class OfficeController : ControllerBase
{
    private readonly IOfficeService _officeService;

    public OfficeController(IOfficeService officeService)
    {
        _officeService = officeService;
    }

    /// <summary>
    /// Obtiene la información de cada consultorio activo.
    /// </summary>
    [HttpGet]
    public async Task<IEnumerable<OfficeGetDto>> Get()
        => await _officeService.GetOfficesAsync();

    /// <summary>
    /// Obtiene la información de cada consultorio activo e inactivo.
    /// </summary>
    [HttpGet("all")]
    public async Task<IEnumerable<OfficeGetDto>> GetAll()
        => await _officeService.GetAllOfficesAsync();

    /// <summary>
    /// Obtiene la información de cada consultorio para el formulario de editar.
    /// </summary>
    [Route("edit")]
    [HttpGet]
    public async Task<IEnumerable<OfficeShowDto>> GetOfficesForEdit()
        => await _officeService.GetOfficesForEditAsync();

    /// <summary>
    /// Crea un nuevo consultorio.
    /// </summary>
    [AuthorizeByRole(RolesName.Superadmin)]
    [HttpPost]
    public async Task<ActionResult<Response>> Post([FromBody]OfficeInsertDto officeInsertDto)
    {
        var response = await _officeService.CreateOfficeAsync(officeInsertDto);
        if (response.Success)
            return CreatedAtAction(nameof(Post), response);

        return BadRequest(response);
    }

    /// <summary>
    /// Actualiza la información del consultorio.
    /// </summary>
    [AuthorizeByRole(RolesName.Superadmin)]
    [HttpPut("{id}")]
    public async Task<ActionResult<Response>> Put(int id, [FromBody]OfficeUpdateDto officeUpdateDto)
    {
        var response = await _officeService.UpdateOfficeAsync(id, User.GetEmployeeId(), officeUpdateDto);
        if (response.Success)
            return Ok(response);

        return NotFound(response);
    }
}
