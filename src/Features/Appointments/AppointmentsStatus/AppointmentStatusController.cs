namespace DentallApp.Features.Appointments.AppointmentsStatus;

[Route("appointment-status")]
[ApiController]
public class AppointmentStatusController : ControllerBase
{
    private readonly AppDbContext _context;

    public AppointmentStatusController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IEnumerable<AppointmentStatusGetDto>> Get()
        => await _context.Set<AppointmentStatus>()
                         .Select(appointmentStatus => appointmentStatus.MapToAppointmentStatusGetDto())
                         .ToListAsync();
}
