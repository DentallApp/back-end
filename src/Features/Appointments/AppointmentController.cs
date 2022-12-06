namespace DentallApp.Features.Appointments;

[Route("appointment")]
[ApiController]
public class AppointmentController : ControllerBase
{
    private readonly IAppointmentService _appointmentService;
    private readonly IAppointmentRepository _appointmentRepository;

    public AppointmentController(IAppointmentService appointmentService, IAppointmentRepository appointmentRepository)
    {
        _appointmentService = appointmentService;
        _appointmentRepository = appointmentRepository;
    }

    /// <summary>
    /// Obtiene el historial de citas del usuario básico.
    /// </summary>
    [AuthorizeByRole(RolesName.BasicUser)]
    [HttpGet("basic-user")]
    public async Task<IEnumerable<AppointmentGetByBasicUserDto>> GetAppointmentsByUserId()
        => await _appointmentRepository.GetAppointmentsByUserIdAsync(User.GetUserId());

    /// <summary>
    /// Permite al usuario básico cancelar su cita agendada.
    /// </summary>
    /// <param name="id">El ID de la cita.</param>
    [AuthorizeByRole(RolesName.BasicUser)]
    [HttpDelete("{id}/basic-user")]
    public async Task<ActionResult<Response>> CancelBasicUserAppointment(int id)
    {
        var response = await _appointmentService.CancelBasicUserAppointmentAsync(id, User.GetUserId());
        if (response.Success)
            return Ok(response);

        return BadRequest(response);
    }

    /// <summary>
    /// Crea una cita médica para cualquier persona.
    /// </summary>
    [AuthorizeByRole(RolesName.Secretary)]
    [HttpPost]
    public async Task<ActionResult<Response>> Post([FromBody]AppointmentInsertDto appointmentInsertDto)
    {
        var response = await _appointmentService.CreateAppointmentAsync(appointmentInsertDto);
        if (response.Success)
            return CreatedAtAction(nameof(Post), response);

        return BadRequest(response);
    }

    /// <summary>
    /// Actualiza el estado de una cita por su ID.
    /// </summary>
    [AuthorizeByRole(RolesName.Secretary, RolesName.Dentist, RolesName.Admin, RolesName.Superadmin)]
    [HttpPut("{id}")]
    public async Task<ActionResult<Response>> Put(int id, [FromBody]AppointmentUpdateDto appointmentUpdateDto)
    {
        var response = await _appointmentService.UpdateAppointmentAsync(id, User, appointmentUpdateDto);
        if (response.Success)
            return Ok(response);

        return BadRequest(response);
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
        if (response.Success)
            return Ok(response);

        return BadRequest(response);
    }

    /// <summary>
    /// Obtiene las citas de los odontólogos para un empleado.
    /// </summary>
    /// <remarks>
    /// Detalles a tomar en cuenta:
    /// <para>- Sí <see cref="AppointmentPostDateDto.OfficeId"/> es <c>0</c>, traerá las citas de TODOS los consultorios.</para>
    /// <para>- Sí <see cref="AppointmentPostDateDto.DentistId"/> es <c>0</c>, traerá las citas de TODOS los odontólogos.</para>
    /// <para>- Sí <see cref="AppointmentPostDateDto.StatusId"/> es <c>0</c>, traerá las citas de TODOS los estados.</para>
    /// </remarks>
    [AuthorizeByRole(RolesName.Secretary, RolesName.Dentist, RolesName.Admin, RolesName.Superadmin)]
    [HttpPost("dentist")]
    public async Task<ActionResult<Response<IEnumerable<AppointmentGetByEmployeeDto>>>> GetAppointmentsForEmployee([FromBody]AppointmentPostDateDto appointmentPostDto)
    {
        var response = await _appointmentService.GetAppointmentsForEmployeeAsync(User, appointmentPostDto);
        if (response.Success)
            return Ok(response);

        return BadRequest(response);
    }
}
