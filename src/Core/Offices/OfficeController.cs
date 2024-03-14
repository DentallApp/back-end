using DentallApp.Core.Offices.UseCases;

namespace DentallApp.Core.Offices;

[Route("office")]
[ApiController]
public class OfficeController
{
    /// <summary>
    /// Creates a new dental office.
    /// </summary>
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType<Result>(StatusCodes.Status400BadRequest)]
    [AuthorizeByRole(RoleName.Superadmin)]
    [HttpPost]
    public async Task<Result<CreatedId>> Create(
        [FromBody]CreateOfficeRequest request,
        CreateOfficeUseCase useCase)
        => await useCase.ExecuteAsync(request);

    /// <summary>
    /// Updates a dental office by its ID
    /// </summary>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<Result>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<Result>(StatusCodes.Status404NotFound)]
    [AuthorizeByRole(RoleName.Superadmin)]
    [HttpPut("{id}")]
    public async Task<Result> Update(
        int id,
        [FromBody]UpdateOfficeRequest request,
        UpdateOfficeUseCase useCase)
        => await useCase.ExecuteAsync(id, request);

    /// <summary>
    /// Gets the names of each dental office.
    /// </summary>
    /// <remarks>
    /// Details to consider:
    /// <para>
    /// - If <c>status</c> is <c>null</c>, you will get all active and inactive offices.
    /// </para>
    /// <para>
    /// - If <c>status</c> is <c>true</c>, you will get all active offices.
    /// </para>
    /// <para>
    /// - If <c>status</c> is <c>false</c>, you will get all inactive offices.
    /// </para>
    /// </remarks>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Route("name")]
    [HttpGet]
    public async Task<IEnumerable<GetOfficeNamesResponse>> GetNames(
        bool? status,
        GetOfficeNamesUseCase useCase)
        => await useCase.ExecuteAsync(status);

    /// <summary>
    /// Gets the offices that will be used to edit from a form.
    /// </summary>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Route("edit")]
    [HttpGet]
    public async Task<IEnumerable<GetOfficesToEditResponse>> GetOfficesToEdit(
        GetOfficesToEditUseCase useCase)
        => await useCase.ExecuteAsync();

    /// <summary>
    /// Gets the active offices (including schedules) to be displayed on the home page.
    /// </summary>
    /// <remarks>The dental office must have at least one active schedule.</remarks>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Route("home-page")]
    [HttpGet]
    public async Task<IEnumerable<GetOfficesForHomePageResponse>> GetOfficesForHomePage(
        GetOfficesForHomePageUseCase useCase)
        => await useCase.ExecuteAsync();

    /// <summary>
    /// You will get an overview of the information of each active and inactive office.
    /// </summary>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Route("overview")]
    [HttpGet]
    public async Task<IEnumerable<GetOfficeOverviewResponse>> GetOverview(
        GetOfficeOverviewUseCase useCase)
        => await useCase.ExecuteAsync();
}
