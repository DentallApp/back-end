namespace DentallApp.Features.Appointments.AppointmentsStatus;

public interface IAppointmentStatusRepository : IRepository<AppointmentStatus>
{
    Task<IEnumerable<AppointmentStatusGetDto>> GetAllStatusAsync();
}
