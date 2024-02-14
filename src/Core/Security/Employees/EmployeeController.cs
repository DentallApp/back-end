using DentallApp.Core.Security.Employees.UseCases;

namespace DentallApp.Core.Security.Employees;

[Route("employee")]
[ApiController]
public class EmployeeController : ControllerBase
{
    [AuthorizeByRole(RoleName.Admin, RoleName.Superadmin)]
    [HttpPost]
    public async Task<Result<CreatedId>> Create(
        [FromBody]CreateEmployeeRequest request,
        CreateEmployeeUseCase useCase)
        => await useCase.ExecuteAsync(User, request);

    [AuthorizeByRole(RoleName.Admin, RoleName.Superadmin)]
    [HttpDelete("{id}")]
    public async Task<Result> Delete(
        int id,
        DeleteEmployeeUseCase useCase)
    {
        if (id == User.GetEmployeeId())
            return Result.Forbidden(Messages.CannotRemoveYourOwnProfile);

        return await useCase.ExecuteAsync(id, User);
    }

    [AuthorizeByRole(RoleName.Secretary, RoleName.Dentist, RoleName.Admin, RoleName.Superadmin)]
    [HttpPut]
    public async Task<Result> UpdateCurrentEmployee(
        [FromBody]UpdateCurrentEmployeeRequest request,
        UpdateCurrentEmployeeUseCase useCase)
        => await useCase.ExecuteAsync(User.GetEmployeeId(), request);

    [AuthorizeByRole(RoleName.Admin, RoleName.Superadmin)]
    [HttpPut("{id}")]
    public async Task<Result> UpdateAnyEmployee(
        int id, 
        [FromBody]UpdateAnyEmployeeRequest request,
        UpdateAnyEmployeeUseCase useCase)
    {
        if (User.IsAdmin() && id == User.GetEmployeeId())
            return Result.Forbidden(Messages.CannotEditYourOwnProfile);

        return await useCase.ExecuteAsync(id, User, request);
    }

    [AuthorizeByRole(RoleName.Admin, RoleName.Superadmin)]
    [HttpGet("edit")]
    public async Task<IEnumerable<GetEmployeesToEditResponse>> GetEmployeesToEdit(
        GetEmployeesToEditUseCase useCase)
        => await useCase.ExecuteAsync(User);

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
    [AuthorizeByRole(RoleName.Secretary, RoleName.Admin, RoleName.Superadmin)]
    [HttpPost("dentist")]
    public async Task<ActionResult<IEnumerable<GetDentistsResponse>>> GetDentists(
        [FromBody]GetDentistsRequest request,
        GetDentistsUseCase useCase)
    {
        if (!User.IsSuperAdmin() && User.IsNotInOffice(request.OfficeId))
            return Forbid();

        return Ok(await useCase.ExecuteAsync(request));
    }

    /// <summary>
    /// Obtiene todos los horarios de los empleados.
    /// </summary>
    /// <returns></returns>
    [AuthorizeByRole(RoleName.Secretary, RoleName.Admin)]
    [HttpGet("overview")]
    public async Task<IEnumerable<GetEmployeeOverviewResponse>> GetOverview(
        GetEmployeeOverviewUseCase useCase)
        => await useCase.ExecuteAsync(User.GetOfficeId());
}
