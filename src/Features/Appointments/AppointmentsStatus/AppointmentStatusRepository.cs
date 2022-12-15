namespace DentallApp.Features.Appointments.AppointmentsStatus;

public class AppointmentStatusRepository : Repository<AppointmentStatus>, IAppointmentStatusRepository
{
    public AppointmentStatusRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<AppointmentStatusGetDto>> GetAllStatusAsync()
        => await Context.Set<AppointmentStatus>()
                        .Select(appointmentStatus => appointmentStatus.MapToAppointmentStatusGetDto())
                        .ToListAsync();
}
