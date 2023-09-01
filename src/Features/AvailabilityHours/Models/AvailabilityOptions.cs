namespace DentallApp.Features.AvailabilityHours.Models;

/// <summary>
/// Representa las opciones que se utilizan para el método <see cref="Availability.GetAvailableHours(AvailabilityOptions)"/>.
/// </summary>
public class AvailabilityOptions
{
    /// <summary>
    /// Obtiene o establece la hora de trabajo de inicio del odontólogo.
    /// </summary>
    public TimeSpan DentistStartHour { get; init; }

    /// <summary>
    /// Obtiene o establece la hora de trabajo de finalización del odontólogo.
    /// </summary>
    public TimeSpan DentistEndHour { get; init; }

    /// <summary>
    /// Obtiene o establece el tiempo de duración de un servicio dental (debe estar expresado en minutos).
    /// </summary>
    public TimeSpan ServiceDuration { get; init; }

    /// <summary>
    /// Obtiene o establece una colección con los rangos de tiempos no disponibles.
    /// La colección debe estar ordenada de forma ascendente y no debe tener franjas de horario duplicadas.
    /// </summary>
    public List<UnavailableTimeRangeResponse> Unavailables { get; init; }

    /// <summary>
    /// Obtiene o establece la fecha de la cita.
    /// </summary>
    public DateTime? AppointmentDate { get; init; }

    /// <summary>
    /// Obtiene o establece la fecha y hora actual.
    /// </summary>
    public DateTime CurrentTimeAndDate { get; init; } = DateTime.Now;
}
