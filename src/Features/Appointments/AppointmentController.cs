namespace DentallApp.Features.Appointments;

[Route("appointment")]
[ApiController]
public class AppointmentController : ControllerBase
{
    private readonly AppointmentService _appointmentService;
    private readonly IAppointmentRepository _appointmentRepository;

    public AppointmentController(AppointmentService appointmentService, IAppointmentRepository appointmentRepository)
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
    /// Crea una cita médica para cualquier persona.
    /// </summary>
    [AuthorizeByRole(RolesName.Secretary)]
    [HttpPost]
    public async Task<ActionResult<Response<InsertedIdDto>>> Post([FromBody]AppointmentInsertDto appointmentInsertDto)
    {
        var response = await _appointmentService.CreateAppointmentAsync(appointmentInsertDto);
        return response.Success ? CreatedAtAction(nameof(Post), response) : BadRequest(response);
    }

    /// <summary>
    /// Actualiza el estado de una cita por su ID.
    /// </summary>
    [AuthorizeByRole(RolesName.Secretary, RolesName.Dentist, RolesName.Admin, RolesName.Superadmin)]
    [HttpPut("{id}")]
    public async Task<ActionResult<Response>> Put(int id, [FromBody]AppointmentUpdateDto appointmentUpdateDto)
    {
        var response = await _appointmentService.UpdateAppointmentAsync(id, User, appointmentUpdateDto);
        return response.Success ? Ok(response) : BadRequest(response);
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
        return response.Success ? Ok(response) : BadRequest(response);
    }
}
