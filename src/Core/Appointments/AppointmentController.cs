namespace DentallApp.Core.Appointments;

[Route("appointment")]
[ApiController]
public class AppointmentController
{
    /// <summary>
    /// Crea una cita médica para cualquier persona.
    /// </summary>
    [AuthorizeByRole(RoleName.Secretary)]
    [HttpPost]
    public async Task<Result<CreatedId>> Create(
        [FromBody]CreateAppointmentRequest request, 
        CreateAppointmentUseCase useCase)
        => await useCase.ExecuteAsync(request);

    /// <summary>
    /// Actualiza el estado de una cita por su ID.
    /// </summary>
    [AuthorizeByRole(RoleName.Secretary, RoleName.Dentist, RoleName.Admin, RoleName.Superadmin)]
    [HttpPut("{id}")]
    public async Task<Result> Update(
        int id, 
        [FromBody]UpdateAppointmentRequest request, 
        UpdateAppointmentUseCase useCase)
        => await useCase.ExecuteAsync(id, request);

    /// <summary>
    /// Permite al usuario básico cancelar su cita agendada.
    /// </summary>
    [AuthorizeByRole(RoleName.BasicUser)]
    [HttpDelete("{id}/basic-user")]
    public async Task<Result> CancelBasicUserAppointment(
        int id, 
        CancelBasicUserAppointmentUseCase useCase)
        => await useCase.ExecuteAsync(id);

    /// <summary>
    /// Permite cancelar las citas agendadas de los odontólogos.
    /// </summary>
    /// <remarks>
    /// Detalles a tomar en cuenta:
    /// <para>- El odontólogo solo podrá cancelar sus propias citas.</para>
    /// <para>- La secretaria/admin solo pueden cancelar las citas del consultorio al que pertenecen.</para>
    /// <para>- El superadmin puede cancelar las citas de cualquier consultorio.</para>
    /// </remarks>
    [AuthorizeByRole(RoleName.Secretary, RoleName.Dentist, RoleName.Admin, RoleName.Superadmin)]
    [HttpPost("cancel/dentist")]
    public async Task<Result<CancelAppointmentsResponse>> CancelAppointments(
        [FromBody]CancelAppointmentsRequest request, 
        CancelAppointmentsUseCase useCase)
        => await useCase.ExecuteAsync(request);

    /// <summary>
    /// Obtiene el historial de citas del usuario básico.
    /// </summary>
    [AuthorizeByRole(RoleName.BasicUser)]
    [HttpGet("basic-user")]
    public async Task<IEnumerable<GetAppointmentsByCurrentUserIdResponse>> GetByCurrentUserId(
        GetAppointmentsByCurrentUserIdUseCase useCase)
        => await useCase.ExecuteAsync();

    /// <summary>
    /// Obtiene las citas de los odontólogos para un empleado.
    /// </summary>
    /// <remarks>
    /// Detalles a tomar en cuenta:
    /// <para>- Sí <see cref="GetAppointmentsByDateRangeRequest.OfficeId"/> es <c>0</c>, traerá las citas de TODOS los consultorios.</para>
    /// <para>- Sí <see cref="GetAppointmentsByDateRangeRequest.DentistId"/> es <c>0</c>, traerá las citas de TODOS los odontólogos.</para>
    /// <para>- Sí <see cref="GetAppointmentsByDateRangeRequest.StatusId"/> es <c>0</c>, traerá las citas de TODOS los estados.</para>
    /// </remarks>
    [AuthorizeByRole(RoleName.Secretary, RoleName.Dentist, RoleName.Admin, RoleName.Superadmin)]
    [HttpPost("dentist")]
    public async Task<ListedResult<GetAppointmentsByDateRangeResponse>> GetByDateRange(
        [FromBody]GetAppointmentsByDateRangeRequest request,
        GetAppointmentsByDateRangeUseCase useCase)
        => await useCase.ExecuteAsync(request);

    /// <summary>
    /// Obtiene las horas disponibles para la reserva de una cita.
    /// </summary>
    [AuthorizeByRole(RoleName.Secretary)]
    [Route("available-hours")]
    [HttpPost]
    public async Task<ListedResult<AvailableTimeRangeResponse>> GetAvailableHours(
        [FromBody]AvailableTimeRangeRequest request,
        GetAvailableHoursUseCase useCase)
        => await useCase.ExecuteAsync(request);
}
