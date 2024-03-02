namespace DentallApp.Core.Reports.UseCases.GetMostRequestedServices;

[AuthorizeByRole(RoleName.Admin, RoleName.Superadmin)]
[ApiController]
public class ReportMostRequestedServicesController : ControllerBase
{
    /// <summary>
    /// Obtiene el reporte de los servicios dentales más solicitados.
    /// </summary>
    [HttpPost("report/most-requested/services")]
    public async Task<ListedResult<GetMostRequestedServicesResponse>> Get(
        [FromBody]GetMostRequestedServicesRequest request,
        GetMostRequestedServicesUseCase useCase)
    {
        if (User.IsAdmin() && User.IsNotInOffice(request.OfficeId))
            return Result.Forbidden();

        return await useCase.ExecuteAsync(request);
    }

    /// <summary>
    /// Descarga el reporte sobre los servicios dentales más solicitados.
    /// </summary>
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
