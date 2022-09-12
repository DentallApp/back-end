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
    /// Cancela cualquier cita agendada de un consultorio.
    /// </summary>
    /// <remarks>
    /// Nota: La secretaria y el admin solo pueden cancelar las citas del consultorio al que pertenecen.
    /// </remarks>
    [AuthorizeByRole(RolesName.Secretary, RolesName.Admin)]
    [HttpPost("cancel/office")]
    public async Task<ActionResult<Response>> CancelAppointmentsByOfficeId([FromBody]AppoinmentCancelByEmployeeDto appoinmentCancelDto)
    {
        var response = await _appoinmentService.CancelAppointmentsByOfficeIdAsync(User.GetOfficeId(), appoinmentCancelDto);
        if (response.Success)
            return Ok(response);

        return BadRequest(response);
    }

    /// <summary>
    /// Cancela cualquier cita agendada del odontólogo actual.
    /// </summary>
    /// <remarks>
    /// Nota: El odontólogo solo puede cancelar sus propias citas.
    /// </remarks>
    [AuthorizeByRole(RolesName.Dentist)]
    [HttpPost("cancel/dentist")]
    public async Task<ActionResult<Response>> CancelAppointmentsByDentistId([FromBody]AppoinmentCancelByDentistDto appoinmentCancelDto)
    {
        var response = await _appoinmentService.CancelAppointmentsByDentistIdAsync(User.GetEmployeeId(), appoinmentCancelDto);
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
    /// Obtiene las citas agendadas del consultorio al que pertenece la secretaria o admin.
    /// </summary>
    [AuthorizeByRole(RolesName.Secretary, RolesName.Admin)]
    [HttpPost("scheduled/office")]
    public async Task<IEnumerable<AppoinmentScheduledGetByEmployeeDto>> GetScheduledAppointmentsByOfficeId([FromBody]AppoinmentPostDateDto appoinmentPostDateDto)
        => await _appoinmentService.GetScheduledAppointmentsByOfficeIdAsync(User.GetOfficeId(), appoinmentPostDateDto);

    /// <summary>
    /// Obtiene las citas agendadas para el odontólogo actual.
    /// </summary>
    /// <remarks>El odontólogo solo podrá ver sus citas agendadas.</remarks>
    [AuthorizeByRole(RolesName.Dentist)]
    [HttpPost("scheduled/dentist")]
    public async Task<IEnumerable<AppoinmentScheduledGetByDentistDto>> GetScheduledAppointmentsByDentistId([FromBody]AppoinmentPostDateDto appoinmentPostDateDto)
        => await _appoinmentService.GetScheduledAppointmentsByDentistIdAsync(User.GetEmployeeId(), appoinmentPostDateDto);
}
