namespace DentallApp.Features.EmployeeSchedules;

public interface IEmployeeScheduleRepository : IRepository<EmployeeSchedule>
{
    Task<EmployeeSchedule> GetEmployeeScheduleByIdAsync(int scheduleId);
    Task<IEnumerable<EmployeeScheduleGetDto>> GetEmployeeScheduleByEmployeeIdAsync(int employeeId);
    Task<IEnumerable<EmployeeScheduleGetAllDto>> GetAllEmployeeSchedulesAsync(int officeId);
    Task<EmployeeScheduleByWeekDayDto> GetEmployeeScheduleByWeekDayIdAsync(int employeeId, int weekDayId);
    Task<IEnumerable<WeekDayDto>> GetEmployeeScheduleWithOnlyWeekDayAsync(int employeeId);
}
