namespace DentallApp.Features.EmployeeSchedules;

public interface IEmployeeScheduleRepository : ISoftDeleteRepository<EmployeeSchedule>
{
    Task<EmployeeSchedule> GetEmployeeScheduleByIdAsync(int scheduleId);
    Task<IEnumerable<EmployeeScheduleGetDto>> GetEmployeeScheduleByEmployeeIdAsync(int employeeId);
    Task<IEnumerable<EmployeeScheduleGetAllDto>> GetAllEmployeeSchedulesAsync(int officeId);
    Task<EmployeeScheduleByWeekDayDto> GetEmployeeScheduleByWeekDayIdAsync(int employeeId, int weekDayId);
}
