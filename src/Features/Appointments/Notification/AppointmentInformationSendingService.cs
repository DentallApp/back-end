namespace DentallApp.Features.Appointments.Notification;

public class AppointmentInformationSendingService : IAppointmentInformationSendingService
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IInstantMessaging _instantMessaging;
    private readonly ISpecificTreatmentRepository _treatmentRepository;

    public AppointmentInformationSendingService(IAppointmentRepository appointmentRepository,
                                                IInstantMessaging instantMessaging,
                                                ISpecificTreatmentRepository treatmentRepository)
    {
        _appointmentRepository = appointmentRepository;
        _instantMessaging = instantMessaging;
        _treatmentRepository = treatmentRepository;
    }

    public async Task SendAppointmentInformationAsync(int appointmentId, AppointmentInsertDto appointmentInsertDto)
    {
        // The query is executed in case the scheduling of appointments is done manually by the secretary.
        appointmentInsertDto.RangeToPay ??= await _treatmentRepository.GetTreatmentWithRangeToPayAsync(appointmentInsertDto.GeneralTreatmentId);
        var businessName = EnvReader.Instance[AppSettings.BusinessName];
        var info = await _appointmentRepository.GetAppointmentInformationAsync(appointmentId);
        var msg = string.Format(AppointmentInformationMessageTemplate, 
                                info.PatientName,
                                businessName,
                                info.DentistName,
                                info.OfficeName,
                                info.DentalServiceName,
                                appointmentInsertDto.AppointmentDate.GetDateInSpanishFormat(),
                                appointmentInsertDto.StartHour.GetHourWithoutSeconds(),
                                appointmentInsertDto.RangeToPay?.ToString());
        await _instantMessaging.SendMessageAsync(info.CellPhone, msg);
    }
}
