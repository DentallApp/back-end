namespace DentallApp.Features.Appoinments;

public interface IAppoinmentService
{
    Task<Response> CancelBasicUserAppointmentAsync(int appoinmentId, int currentUserId);
    Task<Response> CreateAppoinmentAsync(AppoinmentInsertDto appoinmentInsertDto);
    Task<Response> UpdateAppoinmentAsync(int id, ClaimsPrincipal currentEmployee, AppoinmentUpdateDto appoinmentUpdateDto);
    Task<Response<AppoinmentsThatCannotBeCanceledDto>> CancelAppointmentsAsync(ClaimsPrincipal currentEmployee, AppoinmentCancelDto appoinmentCancelDto);
    Task<Response<IEnumerable<AppoinmentGetByEmployeeDto>>> GetAppoinmentsForEmployeeAsync(ClaimsPrincipal currentEmployee, AppoinmentPostDateDto appoinmentPostDto);
}
