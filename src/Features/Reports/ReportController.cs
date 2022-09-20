namespace DentallApp.Features.Reports;

[AuthorizeByRole(RolesName.Admin, RolesName.Superadmin)]
[Route("report")]
[ApiController]
public class ReportController : ControllerBase
{
	private readonly IReportQuery _reportQuery;

	public ReportController(IReportQuery reportQuery)
	{
		_reportQuery = reportQuery;
	}

	/// <summary>
	/// Obtiene el reporte de citas asistidas, no asistidas y canceladas por rango de fechas.
	/// </summary>
	[HttpPost("appoinment")]
	public async Task<IEnumerable<ReportGetAppoinmentDto>> GetAppoinmentsByDateRange([FromBody]ReportPostWithStatusDto reportPostFilterDto)
		=> await _reportQuery.GetAppoinmentsByDateRangeAsync(reportPostFilterDto);
}
