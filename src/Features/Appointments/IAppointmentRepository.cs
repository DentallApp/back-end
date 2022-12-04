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
