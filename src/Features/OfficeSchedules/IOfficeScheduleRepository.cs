namespace DentallApp.Features.OfficeSchedules;

public interface IOfficeScheduleRepository : IRepository<OfficeSchedule>
{
    Task<OfficeSchedule> GetOfficeScheduleByIdAsync(int id);
    Task<IEnumerable<OfficeScheduleGetDto>> GetOfficeScheduleByOfficeIdAsync(int officeId);
    Task<IEnumerable<OfficeScheduleGetAllDto>> GetAllOfficeSchedulesAsync();
    Task<IEnumerable<OfficeScheduleShowDto>> GetHomePageSchedulesAsync();
}
