namespace DentallApp.Features.AvailabilityHours.Models;

/// <summary>
/// Representa un rango de tiempo no disponible de una cita.
/// </summary>
public class UnavailableTimeRangeResponse
{
    public TimeSpan StartHour { get; init; }
    public TimeSpan EndHour { get; init; }
}
