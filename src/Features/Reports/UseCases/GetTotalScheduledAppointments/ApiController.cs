namespace DentallApp.Features.Reports.UseCases.GetTotalScheduledAppointments;

[AuthorizeByRole(RoleName.Admin, RoleName.Superadmin)]
public class ReportTotalScheduledAppointmentsController : ControllerBase
{
    /// <summary>
    /// Obtiene el reporte sobre el total de citas agendadas por odontólogo.
    /// </summary>
    [HttpPost("report/appointment/scheduled")]
    public async Task<ActionResult<IEnumerable<GetTotalScheduledAppointmentsResponse>>> Get(
        [FromBody]GetTotalScheduledAppointmentsRequest request,
        GetTotalScheduledAppointmentsUseCase useCase)
    {
        if (User.IsAdmin() && User.IsNotInOffice(request.OfficeId))
            return Forbid();

        return Ok(await useCase.ExecuteAsync(request));
    }

    /// <summary>
    /// Descarga el reporte sobre el total de citas agendadas por odontólogo.
    /// </summary>
    [HttpPost("pdf/report/appointment/scheduled")]
    public async Task<ActionResult> DownloadAsPdf(
        [FromBody]DownloadScheduledAppointmentsReportRequest request,
        DownloadScheduledAppointmentsReportUseCase useCase)
    {
        var contents = await useCase.DownloadAsPdfAsync(request);
        return File(contents, "application/pdf", "Reporte sobre el total de citas agendadas.pdf");
    }
}
