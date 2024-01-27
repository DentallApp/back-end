namespace DentallApp.Features.AppointmentScheduling;

/// <summary>
/// Define las acciones necesarias para el agendamiento de una cita.
/// </summary>
[AuthorizeByRole(RoleName.Secretary)]
[Route("scheduling")]
[ApiController]
public class SchedulingController(ISchedulingQueries schedulingQueries)
{
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
    public async Task<List<SchedulingResponse>> GetOffices()
		=> await schedulingQueries.GetOfficesAsync();

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
    public async Task<List<SchedulingResponse>> GetDentalServices()
		=> await schedulingQueries.GetDentalServicesAsync();

    /// <summary>
    /// Obtiene los odontólogos activos de un consultorio para el agendamiento.
    /// El odontólogo debe tener al menos un horario activo.
    /// </summary>
    [HttpGet("dentist")]
    public async Task<List<SchedulingResponse>> GetDentists([FromQuery]SchedulingGetDentistsRequest request)
		=> await schedulingQueries.GetDentistsAsync(request.OfficeId, request.DentalServiceId);
}
