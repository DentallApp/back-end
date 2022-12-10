namespace DentallApp.Features.Appointments;

public interface IAppointmentService
{
    Task<Response> CreateAppointmentAsync(AppointmentInsertDto appointmentInsertDto);
    Task<Response> UpdateAppointmentAsync(int id, ClaimsPrincipal currentEmployee, AppointmentUpdateDto appointmentUpdateDto);
    Task<Response<IEnumerable<AppointmentGetByEmployeeDto>>> GetAppointmentsForEmployeeAsync(ClaimsPrincipal currentEmployee, AppointmentPostDateDto appointmentPostDto);
}
