namespace DentallApp.Features.Appoinments.AvailabilityHours;

/// <summary>
/// Representa un rango de tiempo disponible.
/// </summary>
public class AvailableTimeRangeDto
{
    public TimeSpan StartHour { get; set; }
    public TimeSpan EndHour { get; set; }
}
