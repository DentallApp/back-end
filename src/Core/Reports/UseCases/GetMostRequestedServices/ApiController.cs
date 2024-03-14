namespace DentallApp.Core.Reports.UseCases.GetMostRequestedServices;

[AuthorizeByRole(RoleName.Admin, RoleName.Superadmin)]
[ApiController]
public class ReportMostRequestedServicesController
{
    /// <summary>
    /// Gets the report of the most requested dental services.
    /// </summary>
    /// <remarks>
    /// Details to consider:
    /// <para>
    /// - If <c>OfficeId</c> is <c>0</c>, it will get the most requested services from all dental offices.
    /// </para>
    /// </remarks>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<Result>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<Result>(StatusCodes.Status403Forbidden)]
    [HttpPost("report/most-requested/services")]
    public async Task<ListedResult<GetMostRequestedServicesResponse>> Get(
        [FromBody]GetMostRequestedServicesRequest request,
        GetMostRequestedServicesUseCase useCase)
        => await useCase.ExecuteAsync(request);

    /// <summary>
    /// Downloads the report on the most requested dental services in PDF format.
    /// </summary>
    [SwaggerResponse(StatusCodes.Status200OK, type: typeof(byte[]), contentTypes: MediaTypeNames.Application.Pdf)]
    [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(Result), contentTypes: MediaTypeNames.Application.Json)]
    [HttpPost("pdf/report/most-requested/services")]
    public async Task<ActionResult> DownloadAsPdf(
        [FromBody]DownloadDentalServicesReportRequest request,
        DownloadDentalServicesReportUseCase useCase)
    {
        return (await useCase.DownloadAsPdfAsync(request))
            .ToActionResult()
            .Result;
    }
}
