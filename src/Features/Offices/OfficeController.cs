using DentallApp.Features.Offices.UseCases;

namespace DentallApp.Features.Offices;

[Route("office")]
[ApiController]
public class OfficeController : ControllerBase
{
    /// <summary>
    /// Crea un nuevo consultorio.
    /// </summary>
    [AuthorizeByRole(RolesName.Superadmin)]
    [HttpPost]
    public async Task<ActionResult<Response<InsertedIdDto>>> Create(
        [FromBody]CreateOfficeRequest request,
        [FromServices]CreateOfficeUseCase useCase)
    {
        var response = await useCase.Execute(request);
        return response.Success ? CreatedAtAction(nameof(Create), response) : BadRequest(response);
    }

    /// <summary>
    /// Actualiza la información del consultorio.
    /// </summary>
    [AuthorizeByRole(RolesName.Superadmin)]
    [HttpPut("{id}")]
    public async Task<ActionResult<Response>> Update(
        int id,
        [FromBody]UpdateOfficeRequest request,
        [FromServices]UpdateOfficeUseCase useCase)
    {
        var response = await useCase.Execute(id, User.GetEmployeeId(), request);
        return response.Success ? Ok(response) : NotFound(response);
    }

    /// <summary>
    /// Obtiene los nombres de cada consultorio.
    /// </summary>
    /// <remarks>
    /// Detalles a tomar en cuenta:
    /// <para>- Sí <c>status</c> es <c>null</c>, traerá TODOS los consultorios activo e inactivo.</para>
    /// <para>- Sí <c>status</c> es <c>true</c>, traerá los consultorios activos.</para>
    /// <para>- Sí <c>status</c> es <c>false</c>, traerá los consultorios inactivos.</para>
    /// </remarks>
    [Route("name")]
    public async Task<IEnumerable<GetOfficeNamesResponse>> GetOfficeNames(
        bool? status,
        [FromServices]GetOfficeNamesUseCase useCase)
    {
        return await useCase.Execute(status);
    }

    /// <summary>
    /// Obtiene la información de cada consultorio para el formulario de editar.
    /// </summary>
    [Route("edit")]
    [HttpGet]
    public async Task<IEnumerable<GetOfficesToEditResponse>> GetOfficesToEdit(
        [FromServices]GetOfficesToEditUseCase useCase)
    {
        return await useCase.Execute();
    }

    /// <summary>
    /// Obtiene los consultorios activos (incluyendo los horarios) para la página de inicio.
    /// </summary>
    /// <remarks>El consultorio debe tener al menos un horario activo.</remarks>
    [Route("home-page")]
    [HttpGet]
    public async Task<IEnumerable<GetOfficesForHomePageResponse>> Home(
        [FromServices]GetOfficesForHomePageUseCase useCase)
    { 
        return await useCase.Execute();
    }

    /// <summary>
    /// Obtiene una vista general de la información de cada consultorio activo e inactivo.
    /// </summary>
    [HttpGet]
    public async Task<IEnumerable<GetOverviewOfOfficesResponse>> GetOverview(
        [FromServices]GetOverviewOfOfficesUseCase useCase)
    {
        return await useCase.Execute();
    }
}
