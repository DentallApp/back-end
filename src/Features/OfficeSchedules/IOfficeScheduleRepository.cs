namespace DentallApp.Features.OfficeSchedules;

public interface IOfficeScheduleRepository : ISoftDeleteRepository<OfficeSchedule>
{
    Task<OfficeSchedule> GetOfficeScheduleByIdAsync(int id);
    Task<IEnumerable<OfficeScheduleGetDto>> GetOfficeScheduleByOfficeIdAsync(int officeId);
    Task<IEnumerable<OfficeScheduleGetAllDto>> GetAllOfficeSchedulesAsync();
}
