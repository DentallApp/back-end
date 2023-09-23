namespace DentallApp.Shared.Persistence.Repositories;

public interface IAppointmentRepository
{
    Task<int> CancelAppointmentsByOfficeIdAsync(int officeId, IEnumerable<int> appointmentsId);
    Task<int> CancelAppointmentsByDentistIdAsync(int dentistId, IEnumerable<int> appointmentsId);
}
