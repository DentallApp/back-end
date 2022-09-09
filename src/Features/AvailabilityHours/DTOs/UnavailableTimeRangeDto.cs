namespace DentallApp.Features.AvailabilityHours.DTOs;

/// <summary>
/// Representa un rango de tiempo no disponible de una cita.
/// </summary>
public class UnavailableTimeRangeDto
{
    public TimeSpan StartHour { get; set; }
    public TimeSpan EndHour { get; set; }
}
