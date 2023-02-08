namespace DentallApp.Features.Employees;

[Route("employee")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly EmployeeService _employeeService;
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeController(EmployeeService employeeService, IEmployeeRepository employeeRepository)
    {
        _employeeService = employeeService;
        _employeeRepository = employeeRepository;
    }

    [AuthorizeByRole(RolesName.Admin, RolesName.Superadmin)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<Response>> Delete(int id)
    {
        if (id == User.GetEmployeeId())
            return BadRequest(new Response(CannotRemoveYourOwnProfileMessage));

        var response = await _employeeService.RemoveEmployeeAsync(id, User);
        return response.Success ? Ok(response) : BadRequest(response);
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
        return response.Success ? Ok(response) : BadRequest(response);
    }

    [AuthorizeByRole(RolesName.Admin, RolesName.Superadmin)]
    [HttpPut("{id}")]
    public async Task<ActionResult<Response>> Put(int id, [FromBody]EmployeeUpdateByAdminDto employeeUpdateDto)
    {
        if (User.IsAdmin() && id == User.GetEmployeeId())
            return BadRequest(new Response(CannotEditYourOwnProfileMessage));

        var response = await _employeeService.EditProfileByAdminAsync(id, User, employeeUpdateDto);
        return response.Success ? Ok(response) : BadRequest(response);
    }

    /// <summary>
    /// Obtiene los odontólogos de un consultorio.
    /// </summary>
    /// <remarks>
    /// Detalles a tomar en cuenta:
    /// <para>- Sí <see cref="EmployeePostByDentistDto.OfficeId"/> es <c>0</c>, traerá los odontólogos de todos los consultorios.</para>
    /// <para>- Sí <see cref="EmployeePostByDentistDto.IsDentistDeleted"/> es <c>true</c>, traerá los odontólogos que han sido eliminados temporalmente.</para>
    /// <para>- Sí <see cref="EmployeePostByDentistDto.IsDentistDeleted"/> es <c>false</c>, traerá los odontólogos que no han sido eliminados temporalmente.</para>
    /// <para>- Sí <see cref="EmployeePostByDentistDto.IsDentistDeleted"/> es <c>null</c>, traerá TODOS los odontólogos, sin importar si está eliminado temporalmente o no.</para>
    /// </remarks>
    [AuthorizeByRole(RolesName.Secretary, RolesName.Admin, RolesName.Superadmin)]
    [HttpPost("dentist")]
    public async Task<ActionResult<IEnumerable<EmployeeGetByDentistDto>>> GetDentists(EmployeePostByDentistDto employeePostDto)
    {
        if (!User.IsSuperAdmin() && User.IsNotInOffice(employeePostDto.OfficeId))
            return Unauthorized();
        return Ok(await _employeeRepository.GetDentistsAsync(employeePostDto));
    }
}
