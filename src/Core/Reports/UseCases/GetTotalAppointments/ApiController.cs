namespace DentallApp.Core.Reports.UseCases.GetTotalAppointments;

[AuthorizeByRole(RoleName.Admin, RoleName.Superadmin)]
[ApiController]
public class ReportTotalAppointmentsController
{
    /// <summary>
    /// Obtiene el reporte sobre el total de citas asistidas, no asistidas y canceladas.
    /// </summary>
    [HttpPost("report/appointment")]
    public async Task<Result<GetTotalAppointmentsResponse>> Get(
        [FromBody]GetTotalAppointmentsRequest request,
        GetTotalAppointmentsUseCase useCase)
        => await useCase.ExecuteAsync(request);

    /// <summary>
    /// Descarga el reporte sobre el total de citas asistidas, no asistidas y canceladas.
    /// </summary>
    [HttpPost("pdf/report/appointment")]
    public async Task<ActionResult> DownloadAsPdf(
        [FromBody]DownloadTotalAppointmentsReportRequest request,
        DownloadTotalAppointmentsReportUseCase useCase)
    {
        return (await useCase.DownloadAsPdfAsync(request))
            .ToActionResult()
            .Result;
    }
}
