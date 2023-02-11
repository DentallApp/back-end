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
	[HttpPost("appointment")]
	public async Task<ActionResult<ReportGetTotalAppointmentResponse>> GetTotalAppointmentsByDateRange([FromBody]ReportPostWithDentistDto reportPostDto)
	{
		if (User.IsAdmin() && User.IsNotInOffice(reportPostDto.OfficeId))
			return Unauthorized();
		return Ok(await _reportQuery.GetTotalAppointmentsByDateRangeAsync(reportPostDto));
	}

	/// <summary>
	/// Obtiene el reporte sobre el total de citas agendadas por odontólogo.
	/// </summary>
	[HttpPost("appointment/scheduled")]
	public async Task<ActionResult<IEnumerable<ReportGetTotalScheduledAppointmentDto>>> GetTotalScheduledAppointmentsByDateRange([FromBody]ReportPostDto reportPostDto)
	{
        if (User.IsAdmin() && User.IsNotInOffice(reportPostDto.OfficeId))
            return Unauthorized();
        return Ok(await _reportQuery.GetTotalScheduledAppointmentsByDateRangeAsync(reportPostDto));
	}

	/// <summary>
	/// Obtiene el reporte de los servicios dentales más solicitados.
	/// </summary>
	[HttpPost("most-requested/services")]
	public async Task<ActionResult<IEnumerable<ReportGetMostRequestedServicesResponse>>> GetMostRequestedServices([FromBody]ReportPostDto reportPostDto)
	{
        if (User.IsAdmin() && User.IsNotInOffice(reportPostDto.OfficeId))
            return Unauthorized();
        return Ok(await _reportQuery.GetMostRequestedServicesAsync(reportPostDto));
	}
}
