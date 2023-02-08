namespace DentallApp.Features.AppointmentCancellation;

[Route("appointment")]
[ApiController]
public class AppointmentCancellationController : ControllerBase
{
    private readonly AppointmentCancellationService _appointmentService;

    public AppointmentCancellationController(AppointmentCancellationService appointmentService)
    {
        _appointmentService = appointmentService;
    }

    /// <summary>
    /// Permite al usuario básico cancelar su cita agendada.
    /// </summary>
    /// <param name="id">El ID de la cita.</param>
    [AuthorizeByRole(RolesName.BasicUser)]
    [HttpDelete("{id}/basic-user")]
    public async Task<ActionResult<Response>> CancelBasicUserAppointment(int id)
    {
        var response = await _appointmentService.CancelBasicUserAppointmentAsync(id, User.GetUserId());
        return response.Success ? Ok(response) : BadRequest(response);
    }

    /// <summary>
    /// Permite cancelar las citas agendadas de los odontólogos.
    /// </summary>
    /// <remarks>
    /// Detalles a tomar en cuenta:
    /// <para>- El odontólogo solo podrá cancelar sus propias citas.</para>
    /// <para>- La secretaria/admin solo pueden cancelar las citas del consultorio al que pertenecen.</para>
    /// <para>- El superadmin puede cancelar las citas de cualquier consultorio.</para>
    /// </remarks>
    [AuthorizeByRole(RolesName.Secretary, RolesName.Dentist, RolesName.Admin, RolesName.Superadmin)]
    [HttpPost("cancel/dentist")]
    public async Task<ActionResult<Response<AppointmentsThatCannotBeCanceledDto>>> CancelAppointments([FromBody]AppointmentCancelDto appointmentCancelDto)
    {
        var response = await _appointmentService.CancelAppointmentsAsync(User, appointmentCancelDto);
        return response.Success ? Ok(response) : BadRequest(response);
    }
}
