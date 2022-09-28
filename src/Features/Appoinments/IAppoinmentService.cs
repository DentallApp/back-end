namespace DentallApp.Features.Appoinments;

public interface IAppoinmentService
{
    Task<IEnumerable<AppoinmentGetByBasicUserDto>> GetAppoinmentsByUserIdAsync(int userId);
    Task<Response> CancelBasicUserAppointmentAsync(int appoinmentId, int currentUserId);
    Task<Response> CreateAppoinmentAsync(AppoinmentInsertDto appoinmentInsertDto);
    Task<Response> UpdateAppoinmentAsync(int id, ClaimsPrincipal currentEmployee, AppoinmentUpdateDto appoinmentUpdateDto);
    Task<Response> CancelAppointmentsAsync(ClaimsPrincipal currentEmployee, AppoinmentCancelDto appoinmentCancelDto);
    Task<Response<IEnumerable<AppoinmentGetByEmployeeDto>>> GetAppoinmentsForEmployeeAsync(ClaimsPrincipal currentEmployee, AppoinmentPostDateDto appoinmentPostDto);
}
