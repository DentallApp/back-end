namespace DentallApp.Features.Appoinments;

public static class AppoinmentExtensions
{
    /// <summary>
    /// Comprueba sí la cita médica no está cancelada.
    /// </summary>
    /// <returns><c>true</c> sí la cita no está cancelada, de lo contrario devuelve <c>false</c>.</returns>
    [Decompile]
    public static bool IsNotCanceled(this Appoinment appoinment)
        => appoinment.AppoinmentStatusId != AppoinmentStatusId.Canceled;

    [Decompile]
    public static bool HasNotReminder(this Appoinment appoinment)
        => !appoinment.HasReminder;
}

