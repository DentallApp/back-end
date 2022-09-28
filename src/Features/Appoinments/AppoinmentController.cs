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
    public async Task<ActionResult<Response>> Post([FromBody]AppoinmentInsertDto appoinmentInsertDto)
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
    /// Obtiene las citas de los odontólogos para un empleado.
    /// </summary>
    /// <remarks>
    /// Detalles a tomar en cuenta:
    /// <para>- Sí <see cref="AppoinmentPostDateDto.OfficeId"/> es <c>0</c>, traerá las citas de TODOS los consultorios.</para>
    /// <para>- Sí <see cref="AppoinmentPostDateDto.DentistId"/> es <c>0</c>, traerá las citas de TODOS los odontólogos.</para>
    /// <para>- Sí <see cref="AppoinmentPostDateDto.StatusId"/> es <c>0</c>, traerá las citas de TODOS los estados.</para>
    /// </remarks>
    [AuthorizeByRole(RolesName.Secretary, RolesName.Dentist, RolesName.Admin, RolesName.Superadmin)]
    [HttpPost("dentist")]
    public async Task<ActionResult<Response<IEnumerable<AppoinmentGetByEmployeeDto>>>> GetAppointmentsForEmployee([FromBody]AppoinmentPostDateDto appoinmentPostDto)
    {
        var response = await _appoinmentService.GetAppoinmentsForEmployeeAsync(User, appoinmentPostDto);
        if (response.Success)
            return Ok(response);

        return BadRequest(response);
    }
}
