namespace DentallApp.Features.Appointments;

public partial class AppointmentService
{
    private async Task SendAppoinmentInformationAsync(int appoinmentId, AppointmentInsertDto appoinmentInsertDto)
    {
        // La consulta se ejecuta en caso que se realice el agendamiento de forma manual.
        appoinmentInsertDto.RangeToPay ??= await _treatmentRepository.GetTreatmentWithRangeToPayAsync(appoinmentInsertDto.GeneralTreatmentId);
        var businessName = EnvReader.Instance[AppSettings.BusinessName];
        var info = await _appointmentRepository.GetAppointmentInformationAsync(appoinmentId);
        var template = "Hola {0}, gracias por agendar una cita en el consultorio {1}. La información de su cita es:" +
                       "\n- Odontólogo: {2}" +
                       "\n- Consultorio: {3}" +
                       "\n- Servicio dental: {4}" +
                       "\n- Fecha de la cita: {5}" +
                       "\n- Hora de la cita: {6}" +
                       "\n{7}";
        var msg = string.Format(template, info.PatientName,
                                          businessName,
                                          info.DentistName,
                                          info.OfficeName,
                                          info.DentalServiceName,
                                          appoinmentInsertDto.AppointmentDate.GetDateInSpanishFormat(),
                                          appoinmentInsertDto.StartHour.GetHourWithoutSeconds(),
                                          appoinmentInsertDto.RangeToPay?.ToString());
        await _instantMessaging.SendMessageAsync(info.CellPhone, msg);
    }

    private async Task SendMessageAboutAppoinmentCancellationAsync(IEnumerable<AppointmentCancelDetailsDto> appointmentsCanBeCancelled, string reason)
    {
        var businessName = EnvReader.Instance[AppSettings.BusinessName];
        var template = "Estimado usuario {0}, su cita agendada en el consultorio odontológico {1} para el día {2} a las {3} ha sido cancelada por el siguiente motivo: {4}";
        foreach (var appoinment in appointmentsCanBeCancelled)
        {
            var msg = string.Format(template, appoinment.PatientName,
                                              businessName,
                                              appoinment.AppointmentDate.GetDateInSpanishFormat(),
                                              appoinment.StartHour.GetHourWithoutSeconds(),
                                              reason);
            await _instantMessaging.SendMessageAsync(appoinment.PatientCellPhone, msg);
        }
    }
}
