namespace DentallApp.Features.Appoinments;

public partial class AppoinmentService
{
    private async Task SendAppoinmentInformationAsync(int appoinmentId, AppoinmentInsertDto appoinmentInsertDto)
    {
        // La consulta se ejecuta en caso que se realice el agendamiento de forma manual.
        appoinmentInsertDto.RangeToPay ??= await _treatmentRepository.GetTreatmentWithRangeToPayAsync(appoinmentInsertDto.GeneralTreatmentId);
        var businessName = EnvReader.Instance[AppSettings.BusinessName];
        var info = await _appoinmentRepository.GetAppoinmentInformationAsync(appoinmentId);
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
                                          appoinmentInsertDto.AppoinmentDate.GetDateInSpanishFormat(),
                                          appoinmentInsertDto.StartHour.GetHourWithoutSeconds(),
                                          appoinmentInsertDto.RangeToPay?.ToString());
        await _instantMessaging.SendMessageAsync(info.CellPhone, msg);
    }
}
