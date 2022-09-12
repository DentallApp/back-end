namespace DentallApp.Features.Employees;

[Route("employee")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [AuthorizeByRole(RolesName.Admin, RolesName.Superadmin)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<Response>> Delete(int id)
    {
        if (id == User.GetEmployeeId())
            return BadRequest(new Response(CannotRemoveYourOwnProfileMessage));

        var response = await _employeeService.RemoveEmployeeAsync(id, User);
        if (response.Success)
            return Ok(response);

        return BadRequest(response);
    }

    [AuthorizeByRole(RolesName.Admin, RolesName.Superadmin)]
    [HttpGet]
    public async Task<IEnumerable<EmployeeGetDto>> Get()
        => await _employeeService.GetEmployeesAsync(User);

    [AuthorizeByRole(RolesName.Secretary, RolesName.Dentist, RolesName.Admin, RolesName.Superadmin)]
    [HttpPut]
    public async Task<ActionResult<Response>> Put([FromBody]EmployeeUpdateDto employeeUpdateDto)
    { 
        var response = await _employeeService.EditProfileByCurrentEmployeeAsync(User.GetEmployeeId(), employeeUpdateDto);
        if (response.Success)
            return Ok(response);

        return BadRequest(response);
    }

    [AuthorizeByRole(RolesName.Admin, RolesName.Superadmin)]
    [HttpPut("{id}")]
    public async Task<ActionResult<Response>> Put(int id, [FromBody]EmployeeUpdateByAdminDto employeeUpdateDto)
    {
        if (User.IsAdmin() && id == User.GetEmployeeId())
            return BadRequest(new Response(CannotEditYourOwnProfileMessage));

        var response = await _employeeService.EditProfileByAdminAsync(id, User, employeeUpdateDto);
        if (response.Success)
            return Ok(response);

        return BadRequest(response);
    }

    /// <summary>
    /// Obtiene todos los odontólogos activos e inactivos del consultorio al que pertenezca la secretaria o admin.
    /// </summary>
    [AuthorizeByRole(RolesName.Secretary, RolesName.Admin)]
    [HttpGet("dentist/all")]
    public async Task<IEnumerable<EmployeeGetByDentistDto>> GetAllDentists()
        => await _employeeService.GetAllDentistsByOfficeIdAsync(User.GetOfficeId());

    /// <summary>
    /// Obtiene los odontólogos activos del consultorio al que pertenezca la secretaria o admin.
    /// </summary>
    [AuthorizeByRole(RolesName.Secretary, RolesName.Admin)]
    [HttpGet("dentist")]
    public async Task<IEnumerable<EmployeeGetByDentistDto>> GetDentists()
        => await _employeeService.GetDentistsByOfficeIdAsync(User.GetOfficeId());
}
