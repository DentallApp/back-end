namespace DentallApp.Features.Reports.UseCases.GetTotalScheduledAppointments;

[AuthorizeByRole(RolesName.Admin, RolesName.Superadmin)]
public class ReportTotalScheduledAppointmentsController : ControllerBase
{
    /// <summary>
    /// Obtiene el reporte sobre el total de citas agendadas por odontólogo.
    /// </summary>
    [HttpPost("report/appointment/scheduled")]
    public async Task<ActionResult<IEnumerable<GetTotalScheduledAppointmentsResponse>>> Get(
        [FromBody]GetTotalScheduledAppointmentsRequest request,
        [FromServices]GetTotalScheduledAppointmentsUseCase useCase)
    {
        if (User.IsAdmin() && User.IsNotInOffice(request.OfficeId))
            return Unauthorized();

        return Ok(await useCase.Execute(request));
    }

    /// <summary>
    /// Descarga el reporte sobre el total de citas agendadas por odontólogo.
    /// </summary>
    [HttpPost("pdf/report/appointment/scheduled")]
    public async Task<ActionResult> DownloadAsPdf(
        [FromBody]DownloadScheduledAppointmentsReportRequest request,
        [FromServices]DownloadScheduledAppointmentsReportUseCase useCase)
    {
        var contents = await useCase.DownloadAsPdf(request);
        return File(contents, "application/pdf", "Reporte sobre el total de citas agendadas.pdf");
    }
}
