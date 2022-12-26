namespace DentallApp.Features.AppointmentScheduling;

/// <summary>
/// Define las acciones necesarias para el agendamiento de una cita.
/// </summary>
[AuthorizeByRole(RolesName.Secretary)]
[Route("scheduling")]
[ApiController]
public class SchedulingController : ControllerBase
{
	private readonly IBotQueryRepository _botQueryRepository;

	public SchedulingController(IBotQueryRepository botQueryRepository)
	{
		_botQueryRepository = botQueryRepository;
	}

    /// <summary>
    /// Obtiene los consultorios activos para el agendamiento.
    /// El consultorio debe tener al menos un horario activo.
    /// </summary>
    /// <remarks>
    /// Solicitud de muestra:
    ///
    ///     GET /office
    ///     {
    ///        "title": "Mapasingue",
    ///        "value": "1"
    ///     }
	/// Nota: La propiedad <c>value</c> almacena el ID del consultorio.
    /// </remarks>
    [HttpGet("office")]
	public async Task<List<AdaptiveChoice>> GetOffices()
		=> await _botQueryRepository.GetOfficesAsync();

    /// <summary>
    /// Obtiene los servicios dentales activos para el agendamiento. 
    /// El servicio dental debe tener al menos un tratamiento específico.
    /// </summary>
    /// <remarks>
    /// Solicitud de muestra:
    ///
    ///     GET /dental-service
    ///     {
    ///        "title": "Ortodoncia/brackets",
    ///        "value": "1"
    ///     }
    /// Nota: La propiedad <c>value</c> almacena el ID del servicio dental.
    /// </remarks>
    [HttpGet("dental-service")]
	public async Task<List<AdaptiveChoice>> GetDentalServices()
		=> await _botQueryRepository.GetDentalServicesAsync();

    /// <summary>
    /// Obtiene los odontólogos activos de un consultorio para el agendamiento.
    /// El odontólogo debe tener al menos un horario activo.
    /// </summary>
    [HttpGet("dentist")]
	public async Task<List<AdaptiveChoice>> GetDentists([FromQuery]SchedulingGetDentistsDto schedulingDto)
		=> await _botQueryRepository.GetDentistsAsync(schedulingDto.OfficeId, schedulingDto.DentalServiceId);
}
