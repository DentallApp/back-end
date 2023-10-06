namespace DentallApp.Features.Reports.UseCases.GetMostRequestedServices;

[AuthorizeByRole(RolesName.Admin, RolesName.Superadmin)]
public class ReportMostRequestedServicesController : ControllerBase
{
    /// <summary>
    /// Obtiene el reporte de los servicios dentales más solicitados.
    /// </summary>
    [HttpPost("report/most-requested/services")]
    public async Task<ActionResult<IEnumerable<GetMostRequestedServicesResponse>>> Get(
        [FromBody]GetMostRequestedServicesRequest request,
        [FromServices]GetMostRequestedServicesUseCase useCase)
    {
        if (User.IsAdmin() && User.IsNotInOffice(request.OfficeId))
            return Unauthorized();

        return Ok(await useCase.ExecuteAsync(request));
    }

    /// <summary>
    /// Descarga el reporte sobre los servicios dentales más solicitados.
    /// </summary>
    [HttpPost("pdf/report/most-requested/services")]
    public async Task<ActionResult> DownloadAsPdf(
        [FromBody]DownloadDentalServicesReportRequest request,
        [FromServices]DownloadDentalServicesReportUseCase useCase)
    {
        var contents = await useCase.DownloadAsPdfAsync(request);
        return File(contents, "application/pdf", "Reporte sobre los servicios mas solicitados.pdf");
    }
}
