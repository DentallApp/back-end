namespace DentallApp.Features.Appoinments;

public interface IAppoinmentService
{
    Task<IEnumerable<AppoinmentGetByBasicUserDto>> GetAppoinmentsByUserIdAsync(int userId);
}
