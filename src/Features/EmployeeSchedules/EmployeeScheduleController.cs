namespace DentallApp.Features.EmployeeSchedules;

[AuthorizeByRole(RolesName.Secretary, RolesName.Admin, RolesName.Superadmin)]
[Route("employee-schedule")]
[ApiController]
public class EmployeeScheduleController : ControllerBase
{
    private readonly IEmployeeScheduleService _employeeScheduleService;

    public EmployeeScheduleController(IEmployeeScheduleService employeeScheduleService)
    {
        _employeeScheduleService = employeeScheduleService;
    }

    [HttpGet]
    public async Task<IEnumerable<EmployeeScheduleGetDto>> Get()
        => await _employeeScheduleService.GetEmployeeSchedulesAsync();

    [HttpPost]
    public async Task<ActionResult<Response>> Post(EmployeeScheduleInsertDto employeeScheduleDto)
    {
        var response = await _employeeScheduleService.CreateEmployeeScheduleAsync(employeeScheduleDto);
        if (response.Success)
            return CreatedAtAction(nameof(Post), response);

        return BadRequest(response);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Response>> Put(int id, EmployeeScheduleUpdateDto employeeScheduleDto)
    {
        var response = await _employeeScheduleService.UpdateEmployeeScheduleAsync(id, employeeScheduleDto);
        if (response.Success)
            return Ok(response);

        return BadRequest(response);
    }
}
