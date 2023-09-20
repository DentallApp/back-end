namespace DentallApp.Shared.Persistence.Repositories;

public interface IAppointmentRepository
{
    Task<int> CancelAppointmentsByOfficeId(int officeId, IEnumerable<int> appointmentsId);
    Task<int> CancelAppointmentsByDentistId(int dentistId, IEnumerable<int> appointmentsId);
}
