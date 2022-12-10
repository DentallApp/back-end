namespace DentallApp.Features.AppointmentCancellation;

public interface IAppointmentCancellationRepository : IRepository<Appointment>
{
    /// <summary>
    /// Cancela una o más citas agendadas de un consultorio.
    /// </summary>
    /// <param name="officeId">El ID del consultorio.</param>
    /// <param name="appointmentsId">Un conjunto de ID de citas a cancelar.</param>
    Task<int> CancelAppointmentsByOfficeIdAsync(int officeId, IEnumerable<int> appointmentsId);

    /// <summary>
    /// Cancela una o más citas agendadas de un dentista.
    /// </summary>
    /// <param name="dentistId">El ID del dentista.</param>
    /// <param name="appointmentsId">Un conjunto de ID de citas a cancelar.</param>
    Task<int> CancelAppointmentsByDentistIdAsync(int dentistId, IEnumerable<int> appointmentsId);
}
