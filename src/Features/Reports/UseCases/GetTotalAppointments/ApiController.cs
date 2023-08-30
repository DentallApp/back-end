namespace DentallApp.Features.Reports.UseCases.GetTotalAppointments;

[AuthorizeByRole(RolesName.Admin, RolesName.Superadmin)]
public class ReportTotalAppointmentsController : ControllerBase
{
    /// <summary>
    /// Obtiene el reporte sobre el total de citas asistidas, no asistidas y canceladas.
    /// </summary>
    [Route("report")]
    [HttpPost("appointment")]
    public async Task<ActionResult<GetTotalAppointmentsResponse>> Get(
        [FromBody]GetTotalAppointmentsRequest request,
        [FromServices]GetTotalAppointmentsUseCase useCase)
    {
        if (User.IsAdmin() && User.IsNotInOffice(request.OfficeId))
            return Unauthorized();

        return Ok(await useCase.Execute(request));
    }

    /// <summary>
    /// Descarga el reporte sobre el total de citas asistidas, no asistidas y canceladas.
    /// </summary>
    [Route("pdf/report")]
    [HttpPost("appointment")]
    public async Task<ActionResult> DownloadAsPdf(
        [FromBody]DownloadTotalAppointmentsReportRequest request,
        [FromServices]DownloadTotalAppointmentsReportUseCase useCase)
    {
        var contents = await useCase.DownloadAsPdf(request);
        return File(contents, "application/pdf", "Reporte sobre el total de citas.pdf");
    }
}
