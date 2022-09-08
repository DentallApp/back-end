namespace DentallApp.Features.Appoinments;

public interface IAppoinmentRepository : IRepository<Appoinment>
{
    Task<IEnumerable<AppoinmentGetByBasicUserDto>> GetAppoinmentsByUserIdAsync(int userId);
    Task<List<UnavailableTimeRangeDto>> GetUnavailableHoursAsync(int dentistId, DateTime appoinmentDate);
}
