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
}
