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

	[HttpPost("appoinment")]
	public async Task<ActionResult> ReportTotalAppoinmentsDownload([FromBody]ReportPostTotalAppoinmentDownloadDto reportPostDownloadDto)
	{
        var contents = await _reportDownloadPdf.CreateReportTotalAppoinmentPdfAsync(reportPostDownloadDto);
        return File(contents, "application/pdf", "Reporte sobre el total de citas.pdf");
    }

    [HttpPost("appoinment/scheduled")]
    public async Task<ActionResult> ReportTotalScheduledAppoinmentDownload([FromBody]ReportPostScheduledDownloadDto reportPostDownloadDto)
    {
        var contents = await _reportDownloadPdf.CreateReportTotalScheduledAppoinmentPdfAsync(reportPostDownloadDto);
        return File(contents, "application/pdf", "Reporte sobre el total de citas agendadas.pdf");
    }

    [HttpPost("most-requested/services")]
    public async Task<ActionResult> ReportDentalServiceDownload([FromBody]ReportPostDentalServiceDto reportPostDto)
    {
        var contents = await _reportDownloadPdf.CreateReportDentalServiceDownloadPdfAsync(reportPostDto);
        return File(contents, "application/pdf", "Reporte sobre los servicios mas solicitados.pdf");
    }
}
