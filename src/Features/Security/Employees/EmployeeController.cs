using DentallApp.Features.Security.Employees.UseCases;

namespace DentallApp.Features.Security.Employees;

[Route("employee")]
[ApiController]
public class EmployeeController : ControllerBase
{
    [AuthorizeByRole(RolesName.Admin, RolesName.Superadmin)]
    [HttpPost]
    public async Task<ActionResult<Response<InsertedIdDto>>> Create(
        [FromBody]CreateEmployeeRequest request,
        [FromServices]CreateEmployeeUseCase useCase)
    {
        var response = await useCase.Execute(User, request);
        return response.Success ? CreatedAtAction(nameof(Create), response) : BadRequest(response);
    }

    [AuthorizeByRole(RolesName.Admin, RolesName.Superadmin)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<Response>> Delete(
        int id,
        [FromServices]DeleteEmployeeUseCase useCase)
    {
        if (id == User.GetEmployeeId())
            return BadRequest(new Response(CannotRemoveYourOwnProfileMessage));

        var response = await useCase.Execute(id, User);
        return response.Success ? Ok(response) : BadRequest(response);
    }

    [AuthorizeByRole(RolesName.Secretary, RolesName.Dentist, RolesName.Admin, RolesName.Superadmin)]
    [HttpPut]
    public async Task<ActionResult<Response>> UpdateCurrentEmployee(
        [FromBody]UpdateCurrentEmployeeRequest request,
        [FromServices]UpdateCurrentEmployeeUseCase useCase)
    {
        var response = await useCase.Execute(User.GetEmployeeId(), request);
        return response.Success ? Ok(response) : BadRequest(response);
    }

    [AuthorizeByRole(RolesName.Admin, RolesName.Superadmin)]
    [HttpPut("{id}")]
    public async Task<ActionResult<Response>> UpdateAnyEmployee(
        int id, 
        [FromBody]UpdateAnyEmployeeRequest request,
        [FromServices]UpdateAnyEmployeeUseCase useCase)
    {
        if (User.IsAdmin() && id == User.GetEmployeeId())
            return BadRequest(new Response(CannotEditYourOwnProfileMessage));

        var response = await useCase.Execute(id, User, request);
        return response.Success ? Ok(response) : BadRequest(response);
    }

    [AuthorizeByRole(RolesName.Admin, RolesName.Superadmin)]
    [HttpGet]
    public async Task<IEnumerable<GetEmployeesResponse>> GetAll(
        [FromServices]GetEmployeesUseCase useCase)
    { 
        return await useCase.Execute(User);
    }

    /// <summary>
    /// Obtiene los odontólogos de un consultorio.
    /// </summary>
    /// <remarks>
    /// Detalles a tomar en cuenta:
    /// <para>- Sí <see cref="GetDentistsRequest.OfficeId"/> es <c>0</c>, traerá los odontólogos de todos los consultorios.</para>
    /// <para>- Sí <see cref="GetDentistsRequest.IsDentistDeleted"/> es <c>true</c>, traerá los odontólogos que han sido eliminados temporalmente.</para>
    /// <para>- Sí <see cref="GetDentistsRequest.IsDentistDeleted"/> es <c>false</c>, traerá los odontólogos que no han sido eliminados temporalmente.</para>
    /// <para>- Sí <see cref="GetDentistsRequest.IsDentistDeleted"/> es <c>null</c>, traerá TODOS los odontólogos, sin importar si está eliminado temporalmente o no.</para>
    /// </remarks>
    [AuthorizeByRole(RolesName.Secretary, RolesName.Admin, RolesName.Superadmin)]
    [HttpPost("dentist")]
    public async Task<ActionResult<IEnumerable<GetDentistsResponse>>> GetDentists(
        [FromBody]GetDentistsRequest request,
        [FromServices]GetDentistsUseCase useCase)
    {
        if (!User.IsSuperAdmin() && User.IsNotInOffice(request.OfficeId))
            return Unauthorized();

        return Ok(await useCase.Execute(request));
    }

    /// <summary>
    /// Obtiene todos los horarios de los empleados.
    /// </summary>
    /// <returns></returns>
    [AuthorizeByRole(RolesName.Secretary, RolesName.Admin)]
    [HttpGet("schedules")]
    public async Task<IEnumerable<GetSchedulesByOfficeIdResponse>> GetSchedules(
        [FromServices]GetSchedulesByOfficeIdUseCase useCase)
    {
        return await useCase.Execute(User.GetOfficeId());
    }
}
