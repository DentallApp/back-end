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
	public async Task<IEnumerable<ReportGetAppoinmentDto>> GetAppoinmentsByDateRange([FromBody]ReportPostWithStatusDto reportPostDto)
		=> await _reportQuery.GetAppoinmentsByDateRangeAsync(reportPostDto);

    /// <summary>
    /// Obtiene el reporte de citas agendadas por rango de fechas.
    /// </summary>
    [HttpPost("appoinment/scheduled")]
    public async Task<IEnumerable<ReportGetScheduledAppoinmentDto>> GetScheduledAppoinmentsByDateRange([FromBody]ReportPostWithDentistDto reportPostDto)
        => await _reportQuery.GetScheduledAppoinmentsByDateRangeAsync(reportPostDto);

	[HttpPost("most-requested/services")]
	public async Task<IEnumerable<ReportGetMostRequestedServicesDto>> GetMostRequestedServices([FromBody]ReportPostDto reportPostDto)
		=> await _reportQuery.GetMostRequestedServicesAsync(reportPostDto);
}
