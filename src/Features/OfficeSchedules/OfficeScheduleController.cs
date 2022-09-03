namespace DentallApp.Features.OfficeSchedules;

[Route("office-schedule")]
[ApiController]
public class OfficeScheduleController : ControllerBase
{
    private readonly IOfficeScheduleService _officeScheduleService;

    public OfficeScheduleController(IOfficeScheduleService officeScheduleService)
    {
        _officeScheduleService = officeScheduleService;
    }

    /// <summary>
    /// Obtiene los horarios de los consultorios activos para la página de inicio.
    /// </summary>
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
        => await _officeScheduleService.GetOfficeScheduleByOfficeIdAsync(officeId);

    /// <summary>
    /// Crea un nuevo horario para el consultorio.
    /// </summary>
    [AuthorizeByRole(RolesName.Admin, RolesName.Superadmin)]
    [HttpPost]
    public async Task<ActionResult<Response>> Post([FromBody]OfficeScheduleInsertDto officeScheduleInsertDto)
    {
        var response = await _officeScheduleService.CreateOfficeScheduleAsync(User, officeScheduleInsertDto);
        if (response.Success)
            return CreatedAtAction(nameof(Post), response);

        return BadRequest(response);
    }

    /// <summary>
    /// Actualiza el horario de un consultorio.
    /// </summary>
    [AuthorizeByRole(RolesName.Admin, RolesName.Superadmin)]
    [HttpPut("{scheduleId}")]
    public async Task<ActionResult<Response>> Put(int scheduleId, [FromBody]OfficeScheduleUpdateDto officeScheduleUpdateDto)
    {
        var response = await _officeScheduleService.UpdateOfficeScheduleAsync(scheduleId, User, officeScheduleUpdateDto);
        if (response.Success)
            return Ok(response);

        return BadRequest(response);
    }
}
