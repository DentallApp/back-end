namespace DentallApp.Features.Reports.Pdf;

[AuthorizeByRole(RolesName.Admin, RolesName.Superadmin)]
[Route("pdf/report")]
[ApiController]
public class ReportDownloadPdfController : ControllerBase
{
	private readonly ReportDownloadPdfService _reportDownloadPdf;

	public ReportDownloadPdfController(ReportDownloadPdfService reportDownloadPdf)
	{
		_reportDownloadPdf = reportDownloadPdf;
	}

    /// <summary>
    /// Descarga el reporte sobre el total de citas asistidas, no asistidas y canceladas.
    /// </summary>
	[HttpPost("appointment")]
	public async Task<ActionResult> ReportTotalAppointmentsDownload([FromBody]ReportTotalAppointmentDownloadRequest request)
	{
        var contents = await _reportDownloadPdf.CreateReportTotalAppointmentPdfAsync(request);
        return File(contents, "application/pdf", "Reporte sobre el total de citas.pdf");
    }

    /// <summary>
    /// Descarga el reporte sobre el total de citas agendadas por odontólogo.
    /// </summary>
    [HttpPost("appointment/scheduled")]
    public async Task<ActionResult> ReportTotalScheduledAppointmentDownload([FromBody]ReportPostScheduledDownloadDto reportPostDownloadDto)
    {
        var contents = await _reportDownloadPdf.CreateReportTotalScheduledAppointmentPdfAsync(reportPostDownloadDto);
        return File(contents, "application/pdf", "Reporte sobre el total de citas agendadas.pdf");
    }

    /// <summary>
    /// Descarga el reporte sobre los servicios dentales más solicitados.
    /// </summary>
    [HttpPost("most-requested/services")]
    public async Task<ActionResult> ReportDentalServiceDownload([FromBody]ReportDentalServicesDownloadRequest request)
    {
        var contents = await _reportDownloadPdf.CreateReportDentalServiceDownloadPdfAsync(request);
        return File(contents, "application/pdf", "Reporte sobre los servicios mas solicitados.pdf");
    }
}
