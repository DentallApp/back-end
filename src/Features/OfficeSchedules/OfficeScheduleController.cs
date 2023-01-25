namespace DentallApp.Features.OfficeSchedules;

[Route("office-schedule")]
[ApiController]
public class OfficeScheduleController : ControllerBase
{
    private readonly IOfficeScheduleService _officeScheduleService;
    private readonly IOfficeScheduleRepository _officeScheduleRepository;

    public OfficeScheduleController(IOfficeScheduleService officeScheduleService, 
                                    IOfficeScheduleRepository officeScheduleRepository)
    {
        _officeScheduleService = officeScheduleService;
        _officeScheduleRepository = officeScheduleRepository;
    }

    /// <summary>
    /// Obtiene los horarios de los consultorios activos para la página de inicio.
    /// </summary>
    /// <remarks>El consultorio debe tener al menos un horario activo.</remarks>
    [HttpGet("home-page")]
    public async Task<IEnumerable<OfficeScheduleShowDto>> GetHomePageSchedules()
        => await _officeScheduleService.GetHomePageSchedulesAsync();

    /// <summary>
    /// Obtiene todos los horarios de los consultorios activos e inactivos.
    /// </summary>
    [HttpGet]
    public async Task<IEnumerable<OfficeScheduleGetAllDto>> Get()
        => await _officeScheduleService.GetAllOfficeSchedulesAsync();

    /// <summary>
    /// Obtiene el horario de un consultorio activo o inactivo.
    /// </summary>
    [HttpGet("{officeId}")]
    public async Task<IEnumerable<OfficeScheduleGetDto>> GetByOfficeId(int officeId)
        => await _officeScheduleRepository.GetOfficeScheduleByOfficeIdAsync(officeId);

    /// <summary>
    /// Crea un nuevo horario para el consultorio.
    /// </summary>
    [AuthorizeByRole(RolesName.Admin, RolesName.Superadmin)]
    [HttpPost]
    public async Task<ActionResult<Response<DtoBase>>> Post([FromBody]OfficeScheduleInsertDto officeScheduleInsertDto)
    {
        var response = await _officeScheduleService.CreateOfficeScheduleAsync(User, officeScheduleInsertDto);
        return response.Success ? CreatedAtAction(nameof(Post), response) : BadRequest(response);
    }

    /// <summary>
    /// Actualiza el horario de un consultorio.
    /// </summary>
    [AuthorizeByRole(RolesName.Admin, RolesName.Superadmin)]
    [HttpPut("{scheduleId}")]
    public async Task<ActionResult<Response>> Put(int scheduleId, [FromBody]OfficeScheduleUpdateDto officeScheduleUpdateDto)
    {
        var response = await _officeScheduleService.UpdateOfficeScheduleAsync(scheduleId, User, officeScheduleUpdateDto);
        return response.Success ? Ok(response) : BadRequest(response);
    }
}
