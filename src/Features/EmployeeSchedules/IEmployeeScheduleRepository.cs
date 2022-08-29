namespace DentallApp.Features.EmployeeSchedules;

public interface IEmployeeScheduleRepository : ISoftDeleteRepository<EmployeeSchedule>
{
    Task<EmployeeSchedule> GetEmployeeScheduleByIdAsync(int scheduleId);
    Task<IEnumerable<EmployeeScheduleGetDto>> GetEmployeeScheduleByEmployeeIdAsync(int employeeId);
}
