namespace DentallApp.Features.Appointments;

public interface IAppointmentService
{
    Task<Response> CancelBasicUserAppointmentAsync(int appointmentId, int currentUserId);
    Task<Response> CreateAppointmentAsync(AppointmentInsertDto appointmentInsertDto);
    Task<Response> UpdateAppointmentAsync(int id, ClaimsPrincipal currentEmployee, AppointmentUpdateDto appointmentUpdateDto);
    Task<Response<AppointmentsThatCannotBeCanceledDto>> CancelAppointmentsAsync(ClaimsPrincipal currentEmployee, AppointmentCancelDto appointmentCancelDto);
    Task<Response<IEnumerable<AppointmentGetByEmployeeDto>>> GetAppointmentsForEmployeeAsync(ClaimsPrincipal currentEmployee, AppointmentPostDateDto appointmentPostDto);
}
