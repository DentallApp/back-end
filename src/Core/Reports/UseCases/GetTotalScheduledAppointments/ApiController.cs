namespace DentallApp.Core.Reports.UseCases.GetTotalScheduledAppointments;

[AuthorizeByRole(RoleName.Admin, RoleName.Superadmin)]
[ApiController]
public class ReportTotalScheduledAppointmentsController
{
    /// <summary>
    /// Gets the report on the total number of appointments scheduled by dentist.
    /// </summary>
    /// <remarks>
    /// Details to consider:
    /// <para>
    /// - If <c>OfficeId</c> is <c>0</c>, it will get the total number of appointments scheduled from all dental offices.
    /// </para>
    /// </remarks>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<Result>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<Result>(StatusCodes.Status403Forbidden)]
    [HttpPost("report/appointment/scheduled")]
    public async Task<ListedResult<GetTotalScheduledAppointmentsResponse>> Get(
        [FromBody]GetTotalScheduledAppointmentsRequest request,
        GetTotalScheduledAppointmentsUseCase useCase)
        => await useCase.ExecuteAsync(request);

    /// <summary>
    /// Downloads the report on the total number of appointments scheduled by dentist in PDF format.
    /// </summary>
    [SwaggerResponse(StatusCodes.Status200OK, type: typeof(byte[]), contentTypes: MediaTypeNames.Application.Pdf)]
    [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(Result), contentTypes: MediaTypeNames.Application.Json)]
    [HttpPost("pdf/report/appointment/scheduled")]
    public async Task<ActionResult> DownloadAsPdf(
        [FromBody]DownloadScheduledAppointmentsReportRequest request,
        DownloadScheduledAppointmentsReportUseCase useCase)
    {
        return (await useCase.DownloadAsPdfAsync(request))
            .ToActionResult()
            .Result;
    }
}
