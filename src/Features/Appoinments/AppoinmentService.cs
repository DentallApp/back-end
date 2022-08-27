namespace DentallApp.Features.Appoinments;

public class AppoinmentService : IAppoinmentService
{
    private readonly IAppoinmentRepository _appoinmentRepository;

    public AppoinmentService(IAppoinmentRepository appoinmentRepository)
    {
        _appoinmentRepository = appoinmentRepository;
    }

    public async Task<IEnumerable<AppoinmentGetByBasicUserDto>> GetAppoinmentsByUserIdAsync(int userId)
        => await _appoinmentRepository.GetAppoinmentsByUserIdAsync(userId);
}
