namespace DentallApp.Features.AvailabilityHours.DTOs;

/// <summary>
/// Representa un rango de tiempo disponible.
/// </summary>
public class AvailableTimeRangeDto
{
    public string StartHour { get; set; }
    public string EndHour { get; set; }

    public override string ToString()
        => $"{StartHour} - {EndHour}";
}
