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
	/// Obtiene el reporte sobre el total de citas asistidas, no asistidas y canceladas.
	/// </summary>
	[HttpPost("appoinment")]
	public async Task<ReportGetTotalAppoinmentDto> GetTotalAppoinmentsByDateRange([FromBody]ReportPostWithDentistDto reportPostDto)
		=> await _reportQuery.GetTotalAppoinmentsByDateRangeAsync(reportPostDto);

    /// <summary>
    /// Obtiene el reporte sobre el total de citas agendadas por odontólogo.
    /// </summary>
    [HttpPost("appoinment/scheduled")]
    public async Task<IEnumerable<ReportGetTotalScheduledAppoinmentDto>> GetTotalScheduledAppoinmentsByDateRange([FromBody]ReportPostDto reportPostDto)
        => await _reportQuery.GetTotalScheduledAppoinmentsByDateRangeAsync(reportPostDto);

	/// <summary>
	/// Obtiene el reporte de los servicios dentales más solicitados.
	/// </summary>
	[HttpPost("most-requested/services")]
	public async Task<IEnumerable<ReportGetMostRequestedServicesDto>> GetMostRequestedServices([FromBody]ReportPostDto reportPostDto)
		=> await _reportQuery.GetMostRequestedServicesAsync(reportPostDto);
}
