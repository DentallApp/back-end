namespace DentallApp.Features.Appointments;

public static class AppointmentExtensions
{
    /// <summary>
    /// Comprueba sí la cita médica no está cancelada.
    /// </summary>
    /// <returns><c>true</c> sí la cita no está cancelada, de lo contrario devuelve <c>false</c>.</returns>
    [Decompile]
    public static bool IsNotCanceled(this Appointment appointment)
        => appointment.AppointmentStatusId != AppointmentStatusId.Canceled;
}

