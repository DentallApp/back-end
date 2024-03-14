namespace DentallApp.Core.AppointmentScheduling;

[AuthorizeByRole(RoleName.Secretary)]
[Route("scheduling")]
[ApiController]
public class SchedulingController(ISchedulingQueries schedulingQueries)
{
    /// <summary>
    /// Gets the active offices for scheduling.
    /// The office must have at least one active schedule.
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     GET /office
    ///     {
    ///         "title": "Mapasingue",
    ///         "value": "1"
    ///     }
    ///
    /// Note: The <c>value</c> property stores the ID of the office.
    /// </remarks>
    [HttpGet("office")]
    public async Task<List<SchedulingResponse>> GetOffices()
		=> await schedulingQueries.GetOfficesAsync();

    /// <summary>
    /// Gets the active dental services for scheduling. 
    /// The dental service must have at least one specific treatment.
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET /dental-service
    ///     {
    ///         "title": "Ortodoncia/brackets",
    ///         "value": "1"
    ///     }
    /// 
    /// Note: The <c>value</c> property stores the ID of the dental service.
    /// </remarks>
    [HttpGet("dental-service")]
    public async Task<List<SchedulingResponse>> GetDentalServices()
		=> await schedulingQueries.GetDentalServicesAsync();

    /// <summary>
    /// Gets the active dentists in a office for scheduling.
    /// The dentist must have at least one active schedule.
    /// </summary>
    [HttpGet("dentist")]
    public async Task<List<SchedulingResponse>> GetDentists([FromQuery]SchedulingGetDentistsRequest request)
		=> await schedulingQueries.GetDentistsAsync(request.OfficeId, request.DentalServiceId);
}
