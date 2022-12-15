namespace DentallApp.Features.Appointments.AppointmentsStatus;

[Route("appointment-status")]
[ApiController]
public class AppointmentStatusController : ControllerBase
{
    private readonly IAppointmentStatusRepository _repository;

    public AppointmentStatusController(IAppointmentStatusRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IEnumerable<AppointmentStatusGetDto>> Get()
        => await _repository.GetAllStatusAsync();
}
