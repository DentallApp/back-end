namespace DentallApp.Features.Appointments.Notification;

public interface IAppointmentInformationSendingService
{
    Task SendAppointmentInformationAsync(int appointmentId, AppointmentInsertDto appointmentInsertDto);
}
