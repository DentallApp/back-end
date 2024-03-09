namespace DentallApp.Core.Reports.UseCases.GetTotalScheduledAppointments;

[AuthorizeByRole(RoleName.Admin, RoleName.Superadmin)]
[ApiController]
public class ReportTotalScheduledAppointmentsController
{
    /// <summary>
    /// Obtiene el reporte sobre el total de citas agendadas por odontólogo.
    /// </summary>
    [HttpPost("report/appointment/scheduled")]
    public async Task<ListedResult<GetTotalScheduledAppointmentsResponse>> Get(
        [FromBody]GetTotalScheduledAppointmentsRequest request,
        GetTotalScheduledAppointmentsUseCase useCase)
        => await useCase.ExecuteAsync(request);

    /// <summary>
    /// Descarga el reporte sobre el total de citas agendadas por odontólogo.
    /// </summary>
    [HttpPost("pdf/report/appointment/scheduled")]
    public async Task<ActionResult> DownloadAsPdf(
        [FromBody]DownloadScheduledAppointmentsReportRequest request,
        DownloadScheduledAppointmentsReportUseCase useCase)
    {
        return (await useCase.DownloadAsPdfAsync(request))
            .ToActionResult()
            .Result;
    }
}
