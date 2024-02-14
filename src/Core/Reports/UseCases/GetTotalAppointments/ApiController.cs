namespace DentallApp.Core.Reports.UseCases.GetTotalAppointments;

[AuthorizeByRole(RoleName.Admin, RoleName.Superadmin)]
[ApiController]
public class ReportTotalAppointmentsController : ControllerBase
{
    /// <summary>
    /// Obtiene el reporte sobre el total de citas asistidas, no asistidas y canceladas.
    /// </summary>
    [HttpPost("report/appointment")]
    public async Task<ActionResult<GetTotalAppointmentsResponse>> Get(
        [FromBody]GetTotalAppointmentsRequest request,
        GetTotalAppointmentsUseCase useCase)
    {
        if (User.IsAdmin() && User.IsNotInOffice(request.OfficeId))
            return Forbid();

        return Ok(await useCase.ExecuteAsync(request));
    }

    /// <summary>
    /// Descarga el reporte sobre el total de citas asistidas, no asistidas y canceladas.
    /// </summary>
    [HttpPost("pdf/report/appointment")]
    public async Task<ActionResult> DownloadAsPdf(
        [FromBody]DownloadTotalAppointmentsReportRequest request,
        DownloadTotalAppointmentsReportUseCase useCase)
    {
        var contents = await useCase.DownloadAsPdfAsync(request);
        return File(contents, "application/pdf", "Reporte sobre el total de citas.pdf");
    }
}
