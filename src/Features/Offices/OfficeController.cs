namespace DentallApp.Features.Offices;

[Route("office")]
[ApiController]
public class OfficeController : ControllerBase
{
    private readonly IOfficeService _officeService;
    private readonly IOfficeRepository _officeRepository;

    public OfficeController(IOfficeService officeService, IOfficeRepository officeRepository)
    {
        _officeService = officeService;
        _officeRepository = officeRepository;
    }

    /// <summary>
    /// Obtiene la información de cada consultorio activo.
    /// </summary>
    [HttpGet]
    public async Task<IEnumerable<OfficeGetDto>> Get()
        => await _officeRepository.GetOfficesAsync();

    /// <summary>
    /// Obtiene la información de cada consultorio activo e inactivo.
    /// </summary>
    [HttpGet("all")]
    public async Task<IEnumerable<OfficeGetDto>> GetAll()
        => await _officeRepository.GetAllOfficesAsync();

    /// <summary>
    /// Obtiene la información de cada consultorio para el formulario de editar.
    /// </summary>
    [Route("edit")]
    [HttpGet]
    public async Task<IEnumerable<OfficeShowDto>> GetOfficesForEdit()
        => await _officeRepository.GetOfficesForEditAsync();

    /// <summary>
    /// Crea un nuevo consultorio.
    /// </summary>
    [AuthorizeByRole(RolesName.Superadmin)]
    [HttpPost]
    public async Task<ActionResult<Response<DtoBase>>> Post([FromBody]OfficeInsertDto officeInsertDto)
    {
        var response = await _officeService.CreateOfficeAsync(officeInsertDto);
        return response.Success ? CreatedAtAction(nameof(Post), response) : BadRequest(response);
    }

    /// <summary>
    /// Actualiza la información del consultorio.
    /// </summary>
    [AuthorizeByRole(RolesName.Superadmin)]
    [HttpPut("{id}")]
    public async Task<ActionResult<Response>> Put(int id, [FromBody]OfficeUpdateDto officeUpdateDto)
    {
        var response = await _officeService.UpdateOfficeAsync(id, User.GetEmployeeId(), officeUpdateDto);
        return response.Success ? Ok(response) : NotFound(response);
    }
}
