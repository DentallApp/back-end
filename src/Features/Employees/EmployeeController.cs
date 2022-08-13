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
            return Unauthorized();

        var response = await _employeeService.RemoveEmployeeAsync(id, User);
        if (response.Success)
            return Ok(response);

        return BadRequest(response);
    }

    [AuthorizeByRole(RolesName.Admin, RolesName.Superadmin)]
    [HttpGet]
    public async Task<IEnumerable<EmployeeGetDto>> Get()
        => await _employeeService.GetEmployeesAsync(User);
}
