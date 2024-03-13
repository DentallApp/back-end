namespace DentallApp.Core.Appointments;

[Route("appointment")]
[ApiController]
public class AppointmentController
{
    /// <summary>
    /// Creates a medical appointment for a customer.
    /// </summary>
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType<Result>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<Result>(StatusCodes.Status422UnprocessableEntity)]
    [AuthorizeByRole(RoleName.Secretary)]
    [HttpPost]
    public async Task<Result<CreatedId>> Create(
        [FromBody]CreateAppointmentRequest request, 
        CreateAppointmentUseCase useCase)
        => await useCase.ExecuteAsync(request);

    /// <summary>
    /// Updates the status of an appointment by ID.
    /// </summary>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<Result>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<Result>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<Result>(StatusCodes.Status409Conflict)]
    [ProducesResponseType<Result>(StatusCodes.Status403Forbidden)]
    [AuthorizeByRole(RoleName.Secretary, RoleName.Dentist, RoleName.Admin, RoleName.Superadmin)]
    [HttpPut("{id}")]
    public async Task<Result> Update(
        int id, 
        [FromBody]UpdateAppointmentRequest request, 
        UpdateAppointmentUseCase useCase)
        => await useCase.ExecuteAsync(id, request);

    /// <summary>
    /// Allows the current basic user to cancel their scheduled appointment.
    /// </summary>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<Result>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<Result>(StatusCodes.Status403Forbidden)]
    [AuthorizeByRole(RoleName.BasicUser)]
    [HttpDelete("{id}/basic-user")]
    public async Task<Result> CancelBasicUserAppointment(
        int id, 
        CancelBasicUserAppointmentUseCase useCase)
        => await useCase.ExecuteAsync(id);

    /// <summary>
    /// Cancels scheduled appointments of any dentist.
    /// </summary>
    /// <remarks>
    /// Details to consider:
    /// <para>- The dentist may only cancel his own appointments.</para>
    /// <para>- The secretary/admin can only cancel appointments for the office to which they belong.</para>
    /// <para>- The superadmin can cancel appointments for any office.</para>
    /// </remarks>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<Result>(StatusCodes.Status400BadRequest)]
    [AuthorizeByRole(RoleName.Secretary, RoleName.Dentist, RoleName.Admin, RoleName.Superadmin)]
    [HttpPost("cancel/dentist")]
    public async Task<Result<CancelAppointmentsResponse>> CancelAppointments(
        [FromBody]CancelAppointmentsRequest request, 
        CancelAppointmentsUseCase useCase)
        => await useCase.ExecuteAsync(request);

    /// <summary>
    /// Gets the appointment history of the current basic user.
    /// </summary>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [AuthorizeByRole(RoleName.BasicUser)]
    [HttpGet("basic-user")]
    public async Task<IEnumerable<GetAppointmentsByCurrentUserIdResponse>> GetByCurrentUserId(
        GetAppointmentsByCurrentUserIdUseCase useCase)
        => await useCase.ExecuteAsync();

    /// <summary>
    /// Gets the appointments from a specified date range.
    /// </summary>
    /// <remarks>
    /// Details to consider:
    /// <para>- If <c>OfficeId</c> is <c>0</c>, it will retrieve appointments from all offices.</para>
    /// <para>- If <c>DentistId</c> is <c>0</c>, it will retrieve appointments from all dentists.</para>
    /// <para>- If <c>StatusId</c> is <c>0</c>, it will retrieve appointments from all statuses.</para>
    /// </remarks>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<Result>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<Result>(StatusCodes.Status403Forbidden)]
    [AuthorizeByRole(RoleName.Secretary, RoleName.Dentist, RoleName.Admin, RoleName.Superadmin)]
    [HttpPost("dentist")]
    public async Task<ListedResult<GetAppointmentsByDateRangeResponse>> GetByDateRange(
        [FromBody]GetAppointmentsByDateRangeRequest request,
        GetAppointmentsByDateRangeUseCase useCase)
        => await useCase.ExecuteAsync(request);

    /// <summary>
    /// Gets the available hours for the reservation of an appointment.
    /// </summary>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<Result>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<Result>(StatusCodes.Status422UnprocessableEntity)]
    [AuthorizeByRole(RoleName.Secretary)]
    [Route("available-hours")]
    [HttpPost]
    public async Task<ListedResult<AvailableTimeRangeResponse>> GetAvailableHours(
        [FromBody]AvailableTimeRangeRequest request,
        GetAvailableHoursUseCase useCase)
        => await useCase.ExecuteAsync(request);
}
