namespace DentallApp.Features.Appoinments;

public interface IAppoinmentService
{
    Task<IEnumerable<AppoinmentGetByBasicUserDto>> GetAppoinmentsByUserIdAsync(int userId);
    Task<Response> CancelBasicUserAppointmentAsync(int appoinmentId, int currentUserId);
    Task<Response> CreateAppoinmentAsync(AppoinmentInsertDto appoinmentInsertDto); 
}
