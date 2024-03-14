namespace DentallApp.Core.Appointments.UseCases.GetAvailableHours;

/// <summary>
/// Represents the availability of hours for appointment scheduling.
/// </summary>
public static class Availability
{
    /// <summary>
    /// Checks if the new start and end hour is not available for an appointment booking.
    /// </summary>
    /// <remarks>
    /// The method will check if the new time slot conflicts with the occupied time slot.
    /// </remarks>
    /// <param name="newStartHour">The new start hour generated.</param>
    /// <param name="newEndHour">The new end hour generated.</param>
    /// <param name="unavailableTimeRange">An instance with the time range not available.</param>
    /// <returns>
    /// true if the new time slot is not available, otherwise it returns false.
    /// </returns>
    public static bool IsNotAvailable(ref TimeSpan newStartHour, ref TimeSpan newEndHour, UnavailableTimeRangeResponse unavailableTimeRange)
        => unavailableTimeRange.StartHour < newEndHour && newStartHour < unavailableTimeRange.EndHour;

    /// <summary>
    /// Calculate the hours available to book a medical appointment.
    /// </summary>
    /// <param name="options">
    /// An instance with the required options to obtain the available hours.
    /// </param>
    /// <returns>
    /// A collection with the available hours, otherwise returns <c>null</c>.
    /// </returns>
    public static List<AvailableTimeRangeResponse> CalculateAvailableHours(AvailabilityOptions options)
    {
        if (options.ServiceDuration == TimeSpan.Zero)
            throw new InvalidOperationException("The duration of the dental service may not be 00:00");

        var availableHours                   = new List<AvailableTimeRangeResponse>();
        int unavailableTimeRangeIndex        = 0;
        int totalUnavailableHours            = options.Unavailables.Count;
        // To verify if the date of the appointment is not the current date.
        bool appointmentDateIsNotCurrentDate = options.CurrentTimeAndDate.Date != options.AppointmentDate;
        TimeSpan currentTime                 = options.CurrentTimeAndDate.TimeOfDay;
        TimeSpan newStartHour                = options.DentistStartHour;
        while (true)
        {
            TimeSpan newEndHour = newStartHour + options.ServiceDuration;
            if (newEndHour > options.DentistEndHour)
                break;

            var unavailableTimeRange = unavailableTimeRangeIndex >= totalUnavailableHours ? null : options.Unavailables[unavailableTimeRangeIndex];
            if (unavailableTimeRange is not null && IsNotAvailable(ref newStartHour, ref newEndHour, unavailableTimeRange))
            {
                newStartHour = unavailableTimeRange.EndHour;
                unavailableTimeRangeIndex.MoveNextUnavailableTimeRangeIndex();
            }
            else
            {
                if (unavailableTimeRange is not null && newStartHour >= unavailableTimeRange.EndHour)
                    unavailableTimeRangeIndex.MoveNextUnavailableTimeRangeIndex();

                if (appointmentDateIsNotCurrentDate || newStartHour > currentTime)
                {
                    availableHours.Add(new AvailableTimeRangeResponse
                    {
                        StartHour = newStartHour.GetHourWithoutSeconds(),
                        EndHour   = newEndHour.GetHourWithoutSeconds()
                    });
                }
                newStartHour = newEndHour;
            }
        }
        return availableHours.Count == 0 ? null : availableHours;
    }

    /// <summary>
    /// Gets the following unavailable time range index.
    /// </summary>
    private static void MoveNextUnavailableTimeRangeIndex(this ref int i) => i++;
}
