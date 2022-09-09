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
}
