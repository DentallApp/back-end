namespace DentallApp.Features.AppointmentCancellation;

public partial class AppointmentCancellationService
{
    private async Task SendMessageAboutAppointmentCancellationAsync(IEnumerable<AppointmentCancelDetailsDto> appointmentsCanBeCancelled, string reason)
    {
        var businessName = EnvReader.Instance[AppSettings.BusinessName];
        foreach (var appointment in appointmentsCanBeCancelled)
        {
            var msg = string.Format(AppointmentCancellationMessageTemplate, 
                                    appointment.PatientName,
                                    businessName,
                                    appointment.AppointmentDate.GetDateInSpanishFormat(),
                                    appointment.StartHour.GetHourWithoutSeconds(),
                                    reason);
            await _instantMessaging.SendMessageAsync(appointment.PatientCellPhone, msg);
        }
    }
}
