namespace DentallApp.Features.Appoinments;

public interface IAppoinmentRepository : IRepository<Appoinment>
{
    Task<IEnumerable<AppoinmentGetByBasicUserDto>> GetAppoinmentsByUserIdAsync(int userId);
    Task<List<UnavailableTimeRangeDto>> GetUnavailableHoursAsync(int dentistId, DateTime appoinmentDate);

    /// <summary>
    /// Obtiene las citas de los odontólogos para un empleado.
    /// </summary>
    Task<IEnumerable<AppoinmentGetByEmployeeDto>> GetAppoinmentsForEmployeeAsync(AppoinmentPostDateDto appoinmentPostDto);

    /// <summary>
    /// Comprueba sí la fecha y hora de la cita no está disponible.
    /// </summary>
    /// <returns><c>true</c> sí la fecha y hora de la cita no está disponible, de lo contrario devuelve <c>false</c>.</returns>
    Task<bool> IsNotAvailableAsync(AppoinmentInsertDto appoinmentDto);

    /// <summary>
    /// Cancela una o más citas agendadas de un consultorio.
    /// </summary>
    /// <param name="officeId">El ID del consultorio.</param>
    /// <param name="appoinmentsId">Un conjunto de ID de citas a cancelar.</param>
    Task<int> CancelAppointmentsByOfficeIdAsync(int officeId, IEnumerable<int> appoinmentsId);

    /// <summary>
    /// Cancela una o más citas agendadas de un dentista.
    /// </summary>
    /// <param name="dentistId">El ID del dentista.</param>
    /// <param name="appoinmentsId">Un conjunto de ID de citas a cancelar.</param>
    Task<int> CancelAppointmentsByDentistIdAsync(int dentistId, IEnumerable<int> appoinmentsId);
}
