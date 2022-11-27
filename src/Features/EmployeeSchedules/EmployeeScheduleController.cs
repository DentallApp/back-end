namespace DentallApp.Features.EmployeeSchedules;

[AuthorizeByRole(RolesName.Secretary, RolesName.Admin)]
[Route("employee-schedule")]
[ApiController]
public class EmployeeScheduleController : ControllerBase
{
    private readonly IEmployeeScheduleService _employeeScheduleService;
    private readonly IEmployeeScheduleRepository _employeeScheduleRepository;

    public EmployeeScheduleController(IEmployeeScheduleService employeeScheduleService, 
                                      IEmployeeScheduleRepository employeeScheduleRepository)
    {
        _employeeScheduleService = employeeScheduleService;
        _employeeScheduleRepository = employeeScheduleRepository;
    }

    /// <summary>
    /// Obtiene todos los horarios de los empleados.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IEnumerable<EmployeeScheduleGetAllDto>> Get()
        => await _employeeScheduleRepository.GetAllEmployeeSchedulesAsync(User.GetOfficeId());

    /// <summary>
    /// Obtiene el horario de un empleado.
    /// </summary>
    [HttpGet("{employeeId}")]
    public async Task<IEnumerable<EmployeeScheduleGetDto>> GetByEmployeeId(int employeeId)
        => await _employeeScheduleRepository.GetEmployeeScheduleByEmployeeIdAsync(employeeId);

    /// <summary>
    /// Crea un nuevo horario para el empleado.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<Response>> Post([FromBody]EmployeeScheduleInsertDto employeeScheduleDto)
    {
        var response = await _employeeScheduleService.CreateEmployeeScheduleAsync(employeeScheduleDto);
        if (response.Success)
            return CreatedAtAction(nameof(Post), response);

        return BadRequest(response);
    }

    /// <summary>
    /// Actualiza el horario de un empleado.
    /// </summary>
    [HttpPut("{scheduleId}")]
    public async Task<ActionResult<Response>> Put(int scheduleId, [FromBody]EmployeeScheduleUpdateDto employeeScheduleDto)
    {
        var response = await _employeeScheduleService.UpdateEmployeeScheduleAsync(scheduleId, employeeScheduleDto);
        if (response.Success)
            return Ok(response);

        return BadRequest(response);
    }
}
