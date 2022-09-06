namespace DentallApp.Features.Appoinments.AvailabilityHours;

/// <summary>
/// Representa un rango de tiempo no disponible.
/// </summary>
public class UnavailableTimeRange
{
    public TimeSpan StartHour { get; set; }
    public TimeSpan EndHour { get; set; }
}
