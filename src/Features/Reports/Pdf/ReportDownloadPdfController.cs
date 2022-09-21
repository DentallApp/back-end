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
	public async Task<ActionResult> ReportAppoinmentDownload([FromBody]ReportPostAppoinmentDownloadDto reportPostDownloadDto)
	{
        var contents = await _reportDownloadPdf.CreateReportAppoinmentPdfAsync(reportPostDownloadDto);
        return File(contents, "application/pdf", "ReporteCitas.pdf");
    }

    [HttpPost("appoinment/scheduled")]
    public async Task<ActionResult> ReportScheduledAppoinmentDownload([FromBody]ReportPostScheduledDownloadDto reportPostDownloadDto)
    {
        var contents = await _reportDownloadPdf.CreateReportScheduledAppoinmentPdfAsync(reportPostDownloadDto);
        return File(contents, "application/pdf", "ReporteCitasAgendadas.pdf");
    }
}
