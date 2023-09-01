namespace DentallApp.Features.AvailabilityHours.Models;

/// <summary>
/// Representa un rango de tiempo disponible.
/// </summary>
public class AvailableTimeRangeResponse
{
    public string StartHour { get; init; }
    public string EndHour { get; init; }

    public override string ToString()
        => $"{StartHour} - {EndHour}";
}
