namespace DentallApp.Core.Reports.UseCases.GetTotalAppointments;

[AuthorizeByRole(RoleName.Admin, RoleName.Superadmin)]
[ApiController]
public class ReportTotalAppointmentsController
{
    /// <summary>
    /// Gets the report on the total number of appointments attended, not attended and cancelled.
    /// </summary>
    /// <remarks>
    /// Details to consider:
    /// <para>
    /// - If <c>OfficeId</c> is <c>0</c>, it will get the total number of appointments from all dental offices.
    /// </para>
    /// <para>
    /// - If <c>DentistId</c> is <c>0</c>, it will get the total number of appointments from all dentists.
    /// </para>
    /// </remarks>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<Result>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<Result>(StatusCodes.Status403Forbidden)]
    [HttpPost("report/appointment")]
    public async Task<Result<GetTotalAppointmentsResponse>> Get(
        [FromBody]GetTotalAppointmentsRequest request,
        GetTotalAppointmentsUseCase useCase)
        => await useCase.ExecuteAsync(request);

    /// <summary>
    /// Downloads the report on the total number of appointments attended, not attended and cancelled in PDF format.
    /// </summary>
    [SwaggerResponse(StatusCodes.Status200OK, type: typeof(byte[]), contentTypes: MediaTypeNames.Application.Pdf)]
    [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(Result), contentTypes: MediaTypeNames.Application.Json)]
    [HttpPost("pdf/report/appointment")]
    public async Task<ActionResult> DownloadAsPdf(
        [FromBody]DownloadTotalAppointmentsReportRequest request,
        DownloadTotalAppointmentsReportUseCase useCase)
    {
        return (await useCase.DownloadAsPdfAsync(request))
            .ToActionResult()
            .Result;
    }
}
