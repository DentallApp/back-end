namespace DentallApp.Features.AppointmentCancellation;

public interface IAppointmentCancellationService
{
    Task<Response> CancelBasicUserAppointmentAsync(int appointmentId, int currentUserId);
    Task<Response<AppointmentsThatCannotBeCanceledDto>> CancelAppointmentsAsync(ClaimsPrincipal currentEmployee, AppointmentCancelDto appointmentCancelDto);
}
