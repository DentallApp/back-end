namespace DentallApp.Features.AvailabilityHours;

/// <summary>
/// Representa las opciones que se utilizan para el método <see cref="Availability.GetAvailableHours(AvailabilityOptions)"/>.
/// </summary>
public class AvailabilityOptions
{
    /// <summary>
    /// Obtiene o establece la hora de trabajo de inicio del odontólogo.
    /// </summary>
    public TimeSpan DentistStartHour { get; set; }

    /// <summary>
    /// Obtiene o establece la hora de trabajo de finalización del odontólogo.
    /// </summary>
    public TimeSpan DentistEndHour { get; set; }

    /// <summary>
    /// Obtiene o establece el tiempo de duración de un servicio dental (debe estar expresado en minutos).
    /// </summary>
    public TimeSpan ServiceDuration { get; set; }

    /// <summary>
    /// Obtiene o establece una colección con los rangos de tiempos no disponibles.
    /// La colección debe estar ordenada de forma ascendente y no debe tener franjas de horario duplicadas.
    /// </summary>
    public List<UnavailableTimeRangeDto> Unavailables { get; set; }

    /// <summary>
    /// Obtiene o establece la fecha de la cita.
    /// </summary>
    public DateTime? AppoinmentDate { get; set; }

    /// <summary>
    /// Obtiene o establece la fecha y hora actual.
    /// </summary>
    public DateTime CurrentTimeAndDate { get; set; } = DateTime.Now;
}
