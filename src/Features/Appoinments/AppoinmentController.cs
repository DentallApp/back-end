namespace DentallApp.Features.Appoinments;

[Route("appoinment")]
[ApiController]
public class AppoinmentController : ControllerBase
{
    private readonly IAppoinmentService _appoinmentService;

    public AppoinmentController(IAppoinmentService appoinmentService)
    {
        _appoinmentService = appoinmentService;
    }

    /// <summary>
    /// Obtiene el historial de citas del usuario básico.
    /// </summary>
    [AuthorizeByRole(RolesName.BasicUser)]
    [HttpGet("basic-user")]
    public async Task<IEnumerable<AppoinmentGetByBasicUserDto>> GetAppoinmentsByUserId()
        => await _appoinmentService.GetAppoinmentsByUserIdAsync(User.GetUserId());

    /// <summary>
    /// Permite al usuario básico cancelar su cita agendada.
    /// </summary>
    /// <param name="id">El ID de la cita.</param>
    [AuthorizeByRole(RolesName.BasicUser)]
    [HttpDelete("{id}/basic-user")]
    public async Task<ActionResult<Response>> CancelBasicUserAppointment(int id)
    {
        var response = await _appoinmentService.CancelBasicUserAppointmentAsync(id, User.GetUserId());
        if (response.Success)
            return Ok(response);

        return BadRequest(response);
    }

    /// <summary>
    /// Crea una cita médica para cualquier persona.
    /// </summary>
    [AuthorizeByRole(RolesName.Secretary)]
    [HttpPost]
    public async Task<ActionResult<Response>> Post(AppoinmentInsertDto appoinmentInsertDto)
    {
        var response = await _appoinmentService.CreateAppoinmentAsync(appoinmentInsertDto);
        if (response.Success)
            return CreatedAtAction(nameof(Post), response);

        return BadRequest(response);
    }

    /// <summary>
    /// Actualiza el estado de una cita por su ID.
    /// </summary>
    [AuthorizeByRole(RolesName.Secretary, RolesName.Dentist, RolesName.Admin)]
    [HttpPut("{id}")]
    public async Task<ActionResult<Response>> Put(int id, [FromBody]AppoinmentUpdateDto appoinmentUpdateDto)
    {
        var response = await _appoinmentService.UpdateAppoinmentAsync(id, User, appoinmentUpdateDto);
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
    public async Task<ActionResult<Response>> CancelAppointments([FromBody]AppoinmentCancelDto appoinmentCancelDto)
    {
        var response = await _appoinmentService.CancelAppointmentsAsync(User, appoinmentCancelDto);
        if (response.Success)
            return Ok(response);

        return BadRequest(response);
    }

    /// <summary>
    /// Obtiene las citas de cualquier estado (agendada, cancelada, asistida y no asistida) del consultorio al que pertenece la secretaria o admin.
    /// </summary>
    [AuthorizeByRole(RolesName.Secretary, RolesName.Admin)]
    [HttpPost("office")]
    public async Task<IEnumerable<AppoinmentGetByEmployeeDto>> GetAppointmentsByOfficeId([FromBody]AppoinmentPostDateDto appoinmentPostDateDto)
        => await _appoinmentService.GetAppointmentsByOfficeIdAsync(User.GetOfficeId(), appoinmentPostDateDto);

    /// <summary>
    /// Obtiene las citas de cualquier estado (agendada, cancelada, asistida y no asistida) para el odontólogo actual.
    /// </summary>
    /// <remarks>El odontólogo solo podrá ver sus citas programadas.</remarks>
    [AuthorizeByRole(RolesName.Dentist)]
    [HttpPost("dentist")]
    public async Task<IEnumerable<AppoinmentGetByDentistDto>> GetAppointmentsByDentistId([FromBody]AppoinmentPostDateDto appoinmentPostDateDto)
        => await _appoinmentService.GetAppointmentsByDentistIdAsync(User.GetEmployeeId(), appoinmentPostDateDto);

    /// <summary>
    /// Obtiene las citas agendadas de un consultorio.
    /// </summary>
    /// <remarks>
    /// Detalles a tomar en cuenta:
    /// <para>- La secretaria/admin solo pueden obtener las citas agendadas del consultorio al que pertenecen.</para>
    /// <para>- El superadmin puede obtener las citas agendadas de cualquier consultorio.</para>
    /// </remarks>
    [AuthorizeByRole(RolesName.Secretary, RolesName.Admin, RolesName.Superadmin)]
    [HttpPost("scheduled/office/{officeId}")]
    public async Task<ActionResult<Response<IEnumerable<AppoinmentScheduledGetByEmployeeDto>>>> GetScheduledAppointmentsByOfficeId(int officeId, [FromBody]AppoinmentPostDateWithDentistDto appoinmentPostDateDto)
    {
        if (!User.IsSuperAdmin() && User.IsNotInOffice(officeId))
            return BadRequest(new Response(OfficeNotAssignedMessage));
        var data = await _appoinmentService.GetScheduledAppointmentsByOfficeIdAsync(officeId, appoinmentPostDateDto);
        return Ok(new Response { Success = true, Data = data, Message = GetResourceMessage });
    }

    /// <summary>
    /// Obtiene las citas agendadas para el odontólogo actual.
    /// </summary>
    /// <remarks>El odontólogo solo podrá ver sus citas agendadas.</remarks>
    [AuthorizeByRole(RolesName.Dentist)]
    [HttpPost("scheduled/dentist")]
    public async Task<IEnumerable<AppoinmentScheduledGetByDentistDto>> GetScheduledAppointmentsByDentistId([FromBody]AppoinmentPostDateDto appoinmentPostDateDto)
        => await _appoinmentService.GetScheduledAppointmentsByDentistIdAsync(User.GetEmployeeId(), appoinmentPostDateDto);
}
