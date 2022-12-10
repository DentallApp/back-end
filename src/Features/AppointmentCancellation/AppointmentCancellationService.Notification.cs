namespace DentallApp.Features.AppointmentCancellation;

public partial class AppointmentCancellationService
{
    private async Task SendMessageAboutAppointmentCancellationAsync(IEnumerable<AppointmentCancelDetailsDto> appointmentsCanBeCancelled, string reason)
    {
        var businessName = EnvReader.Instance[AppSettings.BusinessName];
        var template = "Estimado usuario {0}, su cita agendada en el consultorio odontológico {1} para el día {2} a las {3} ha sido cancelada por el siguiente motivo: {4}";
        foreach (var appointment in appointmentsCanBeCancelled)
        {
            var msg = string.Format(template, appointment.PatientName,
                                              businessName,
                                              appointment.AppointmentDate.GetDateInSpanishFormat(),
                                              appointment.StartHour.GetHourWithoutSeconds(),
                                              reason);
            await _instantMessaging.SendMessageAsync(appointment.PatientCellPhone, msg);
        }
    }
}
