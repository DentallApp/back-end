namespace DentallApp.Features.Appoinments;

public interface IAppoinmentRepository : IRepository<Appoinment>
{
    Task<IEnumerable<AppoinmentGetByBasicUserDto>> GetAppoinmentsByUserIdAsync(int userId);
    Task<List<UnavailableTimeRangeDto>> GetUnavailableHoursAsync(int dentistId, DateTime appoinmentDate);

    /// <summary>
    /// Comprueba sí la fecha y hora de la cita no está disponible.
    /// </summary>
    /// <returns><c>true</c> sí la fecha y hora de la cita no está disponible, de lo contrario devuelve <c>false</c>.</returns>
    Task<bool> IsNotAvailableAsync(AppoinmentInsertDto appoinmentDto);

    /// <summary>
    /// Obtiene las citas de cualquier estado (agendada, cancelada, asistida y no asistida) de un consultorio.
    /// </summary>
    /// <param name="officeId">El ID del consultorio.</param>
    /// <param name="from">Desde que fecha se desea filtrar las citas.</param>
    /// <param name="to">Hasta que fecha se desea filtrar las citas.</param>
    Task<IEnumerable<AppoinmentGetByEmployeeDto>> GetAppointmentsByOfficeIdAsync(int officeId, DateTime from, DateTime to);

    /// <summary>
    /// Obtiene las citas de cualquier estado (agendada, cancelada, asistida y no asistida) de un dentista.
    /// </summary>
    /// <param name="dentistId">El ID del dentista.</param>
    /// <param name="from">Desde que fecha se desea filtrar las citas.</param>
    /// <param name="to">Hasta que fecha se desea filtrar las citas.</param>
    Task<IEnumerable<AppoinmentGetByDentistDto>> GetAppointmentsByDentistIdAsync(int dentistId, DateTime from, DateTime to);

    /// <summary>
    /// Obtiene las citas agendadas de un consultorio.
    /// </summary>
    /// <param name="officeId">El ID del consultorio.</param>
    /// <param name="from">Desde que fecha se desea filtrar las citas.</param>
    /// <param name="to">Hasta que fecha se desea filtrar las citas.</param>
    Task<IEnumerable<AppoinmentScheduledGetByEmployeeDto>> GetScheduledAppointmentsByOfficeIdAsync(int officeId, DateTime from, DateTime to);

    /// <summary>
    /// Obtiene las citas agendadas de un dentista.
    /// </summary>
    /// <param name="dentistId">El ID del dentista.</param>
    /// <param name="from">Desde que fecha se desea filtrar las citas.</param>
    /// <param name="to">Hasta que fecha se desea filtrar las citas.</param>
    Task<IEnumerable<AppoinmentScheduledGetByDentistDto>> GetScheduledAppointmentsByDentistIdAsync(int dentistId, DateTime from, DateTime to);
}
