using DentallApp.Features.Offices.UseCases;

namespace DentallApp.Features.Offices;

[Route("office")]
[ApiController]
public class OfficeController : ControllerBase
{
    /// <summary>
    /// Crea un nuevo consultorio.
    /// </summary>
    [AuthorizeByRole(RoleName.Superadmin)]
    [HttpPost]
    public async Task<Result<CreatedId>> Create(
        [FromBody]CreateOfficeRequest request,
        CreateOfficeUseCase useCase)
        => await useCase.ExecuteAsync(request);

    /// <summary>
    /// Actualiza la información del consultorio.
    /// </summary>
    [AuthorizeByRole(RoleName.Superadmin)]
    [HttpPut("{id}")]
    public async Task<Result> Update(
        int id,
        [FromBody]UpdateOfficeRequest request,
        UpdateOfficeUseCase useCase)
        => await useCase.ExecuteAsync(id, User.GetEmployeeId(), request);

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
    [HttpGet]
    public async Task<IEnumerable<GetOfficeNamesResponse>> GetNames(
        bool? status,
        GetOfficeNamesUseCase useCase)
        => await useCase.ExecuteAsync(status);

    /// <summary>
    /// Obtiene la información de cada consultorio para el formulario de editar.
    /// </summary>
    [Route("edit")]
    [HttpGet]
    public async Task<IEnumerable<GetOfficesToEditResponse>> GetOfficesToEdit(
        GetOfficesToEditUseCase useCase)
        => await useCase.ExecuteAsync();

    /// <summary>
    /// Obtiene los consultorios activos (incluyendo los horarios) para la página de inicio.
    /// </summary>
    /// <remarks>El consultorio debe tener al menos un horario activo.</remarks>
    [Route("home-page")]
    [HttpGet]
    public async Task<IEnumerable<GetOfficesForHomePageResponse>> GetOfficesForHomePage(
        GetOfficesForHomePageUseCase useCase)
        => await useCase.ExecuteAsync();

    /// <summary>
    /// Obtiene una vista general de la información de cada consultorio activo e inactivo.
    /// </summary>
    [Route("overview")]
    [HttpGet]
    public async Task<IEnumerable<GetOfficeOverviewResponse>> GetOverview(
        GetOfficeOverviewUseCase useCase)
        => await useCase.ExecuteAsync();
}
