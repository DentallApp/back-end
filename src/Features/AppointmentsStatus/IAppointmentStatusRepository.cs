namespace DentallApp.Features.AppointmentsStatus;

public interface IAppointmentStatusRepository : IRepository<AppointmentStatus>
{
    Task<IEnumerable<AppointmentStatusGetDto>> GetAllStatusAsync();
}
