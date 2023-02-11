namespace DentallApp.Features.Reports;

[AuthorizeByRole(RolesName.Admin, RolesName.Superadmin)]
[Route("report")]
[ApiController]
public class ReportController : ControllerBase
{
	private readonly IMediator _mediator;

	public ReportController(IMediator mediator)
		=> _mediator = mediator;

	/// <summary>
	/// Obtiene el reporte sobre el total de citas asistidas, no asistidas y canceladas.
	/// </summary>
	[HttpPost("appointment")]
	public async Task<ActionResult<ReportGetTotalAppointmentResponse>> GetTotalAppointmentsByDateRange([FromBody]ReportGetTotalAppointmentRequest request)
	{
		if (User.IsAdmin() && User.IsNotInOffice(request.OfficeId))
			return Unauthorized();
		return Ok(await _mediator.Send(request));
	}

	/// <summary>
	/// Obtiene el reporte sobre el total de citas agendadas por odontólogo.
	/// </summary>
	[HttpPost("appointment/scheduled")]
	public async Task<ActionResult<IEnumerable<ReportGetTotalScheduledAppointmentResponse>>> GetTotalScheduledAppointmentsByDateRange([FromBody]ReportGetTotalScheduledAppointmentRequest request)
	{
        if (User.IsAdmin() && User.IsNotInOffice(request.OfficeId))
            return Unauthorized();
        return Ok(await _mediator.Send(request));
	}

	/// <summary>
	/// Obtiene el reporte de los servicios dentales más solicitados.
	/// </summary>
	[HttpPost("most-requested/services")]
	public async Task<ActionResult<IEnumerable<ReportGetMostRequestedServiceResponse>>> GetMostRequestedServices([FromBody]ReportGetMostRequestedServicesRequest request)
	{
        if (User.IsAdmin() && User.IsNotInOffice(request.OfficeId))
            return Unauthorized();
        return Ok(await _mediator.Send(request));
	}
}