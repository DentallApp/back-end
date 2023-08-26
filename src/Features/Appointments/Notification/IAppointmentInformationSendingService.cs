using DentallApp.Features.Appointments.UseCases;

namespace DentallApp.Features.Appointments.Notification;

public interface IAppointmentInformationSendingService
{
    Task SendAppointmentInformationAsync(int appointmentId, CreateAppointmentRequest request);
}
