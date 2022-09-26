namespace DentallApp.Features.Reports.Pdf;

[AuthorizeByRole(RolesName.Admin, RolesName.Superadmin)]
[Route("pdf/report")]
[ApiController]
public class ReportDownloadPdfController : ControllerBase
{
	private readonly IReportDownloadPdfService _reportDownloadPdf;

	public ReportDownloadPdfController(IReportDownloadPdfService reportDownloadPdf)
	{
		_reportDownloadPdf = reportDownloadPdf;
	}

    /// <summary>
    /// Descarga el reporte sobre el total de citas asistidas, no asistidas y canceladas.
    /// </summary>
	[HttpPost("appoinment")]
	public async Task<ActionResult> ReportTotalAppoinmentsDownload([FromBody]ReportPostTotalAppoinmentDownloadDto reportPostDownloadDto)
	{
        var contents = await _reportDownloadPdf.CreateReportTotalAppoinmentPdfAsync(reportPostDownloadDto);
        return File(contents, "application/pdf", "Reporte sobre el total de citas.pdf");
    }

    /// <summary>
    /// Descarga el reporte sobre el total de citas agendadas por odontólogo.
    /// </summary>
    [HttpPost("appoinment/scheduled")]
    public async Task<ActionResult> ReportTotalScheduledAppoinmentDownload([FromBody]ReportPostScheduledDownloadDto reportPostDownloadDto)
    {
        var contents = await _reportDownloadPdf.CreateReportTotalScheduledAppoinmentPdfAsync(reportPostDownloadDto);
        return File(contents, "application/pdf", "Reporte sobre el total de citas agendadas.pdf");
    }

    /// <summary>
    /// Descarga el reporte sobre los servicios dentales más solicitados.
    /// </summary>
    [HttpPost("most-requested/services")]
    public async Task<ActionResult> ReportDentalServiceDownload([FromBody]ReportPostDentalServiceDto reportPostDto)
    {
        var contents = await _reportDownloadPdf.CreateReportDentalServiceDownloadPdfAsync(reportPostDto);
        return File(contents, "application/pdf", "Reporte sobre los servicios mas solicitados.pdf");
    }
}
