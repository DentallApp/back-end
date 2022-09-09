namespace DentallApp.Features.AvailabilityHours;

/// <summary>
/// Representa la disponibilidad de horas para el agendamiento de citas.
/// </summary>
public static class Availability
{
    /// <summary>
    /// Comprueba sí la nueva hora de inicio y finalización no están disponible para una reserva de cita.
    /// </summary>
    /// <remarks>El método verificará sí la nueva franja de horario entra en conflicto con la franja de horario ocupada.</remarks>
    /// <param name="newStartHour">La nueva hora de inicio generada.</param>
    /// <param name="newEndHour">La nueva hora de finalización generada.</param>
    /// <param name="unavailableTimeRange">Una instancia con el rango de tiempo no disponible.</param>
    /// <returns><c>true</c> sí la nueva franja de horario no está disponible, de lo contrario devuelve <c>false</c>.</returns>
    private static bool IsNotAvailable(ref TimeSpan newStartHour, ref TimeSpan newEndHour, UnavailableTimeRangeDto unavailableTimeRange)
        => (unavailableTimeRange.StartHour != newEndHour && unavailableTimeRange.StartHour >= newStartHour && unavailableTimeRange.StartHour <= newEndHour) ||
           (unavailableTimeRange.EndHour != newStartHour && unavailableTimeRange.EndHour >= newStartHour && unavailableTimeRange.EndHour <= newEndHour);

    /// <summary>
    /// Obtiene las horas disponibles para la reserva de una cita médica.
    /// </summary>
    /// <param name="options">Una instancia con las opciones requeridas para obtener las horas disponibles.</param>
    /// <returns>Una colección con las horas disponibles, de lo contrario devuelve <c>null</c>.</returns>
    public static List<AvailableTimeRangeDto> GetAvailableHours(AvailabilityOptions options)
    {
        if (options.ServiceDuration == TimeSpan.Zero)
            throw new InvalidOperationException("The duration of the dental service may not be 00:00");

        var availableHours                  = new List<AvailableTimeRangeDto>();
        int unavailableTimeRangeIndex       = 0;
        int totalUnavailableHours           = options.Unavailables.Count;
        // Para verificar sí la fecha de la cita no es la fecha actual.
        bool appoinmentDateIsNotCurrentDate = options.CurrentTimeAndDate.Date != options.AppoinmentDate;
        TimeSpan currentTime                = options.CurrentTimeAndDate.TimeOfDay;
        TimeSpan newStartHour               = options.DentistStartHour;
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

                if (appoinmentDateIsNotCurrentDate || newStartHour > currentTime)
                {
                    availableHours.Add(new AvailableTimeRangeDto
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
    /// Obtiene el siguiente índice de rango de tiempo no disponible.
    /// </summary>
    private static void MoveNextUnavailableTimeRangeIndex(this ref int i) => i++;
}
