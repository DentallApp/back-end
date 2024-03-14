namespace DentallApp.Core.Appointments.UseCases.GetAvailableHours;

/// <summary>
/// Represents the options used for the method called 
/// <see cref="Availability.CalculateAvailableHours(AvailabilityOptions)"/>.
/// </summary>
public class AvailabilityOptions
{
    /// <summary>
    /// Gets or sets the dentist's start hour.
    /// </summary>
    public TimeSpan DentistStartHour { get; init; }

    /// <summary>
    /// Gets or sets the dentist's end hour.
    /// </summary>
    public TimeSpan DentistEndHour { get; init; }

    /// <summary>
    /// Gets or sets the duration time of a dental service (must be expressed in minutes).
    /// </summary>
    public TimeSpan ServiceDuration { get; init; }

    /// <summary>
    /// Gets or sets a collection of unavailable time ranges.
    /// </summary>
    /// <remarks>
    /// The collection should be sorted in ascending order and should not have duplicate time slots.
    /// </remarks>
    public List<UnavailableTimeRangeResponse> Unavailables { get; init; }

    /// <summary>
    /// Gets or sets the date of the appointment.
    /// </summary>
    public DateTime? AppointmentDate { get; init; }

    /// <summary>
    /// Gets or sets the current date and time.
    /// </summary>
    public DateTime CurrentTimeAndDate { get; init; } = DateTime.Now;
}
