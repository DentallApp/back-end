namespace DentallApp.Features.Appointments;

public interface IAppointmentRepository : IRepository<Appointment>
{
    Task<AppointmentInfoDto> GetAppointmentInformationAsync(int id);
    Task<IEnumerable<AppointmentGetByBasicUserDto>> GetAppointmentsByUserIdAsync(int userId);
    Task<List<UnavailableTimeRangeDto>> GetUnavailableHoursAsync(int dentistId, DateTime appointmentDate);

    /// <summary>
    /// Obtiene las citas de los odontólogos para un empleado.
    /// </summary>
    Task<IEnumerable<AppointmentGetByEmployeeDto>> GetAppointmentsForEmployeeAsync(AppointmentPostDateDto appointmentPostDto);

    /// <summary>
    /// Comprueba sí la fecha y hora de la cita no está disponible.
    /// </summary>
    /// <returns><c>true</c> sí la fecha y hora de la cita no está disponible, de lo contrario devuelve <c>false</c>.</returns>
    Task<bool> IsNotAvailableAsync(AppointmentInsertDto appointmentDto);
}
