namespace DentallApp.Features.EmployeeSchedules;

public interface IEmployeeScheduleRepository : ISoftDeleteRepository<EmployeeSchedule>
{
    Task<IEnumerable<EmployeeScheduleGetDto>> GetEmployeeSchedulesAsync();
}
