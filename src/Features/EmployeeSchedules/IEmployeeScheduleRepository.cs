namespace DentallApp.Features.EmployeeSchedules;

public interface IEmployeeScheduleRepository
{
    Task<EmployeeScheduleByWeekDayDto> GetEmployeeScheduleByWeekDayIdAsync(int employeeId, int weekDayId);
    Task<IEnumerable<WeekDayDto>> GetEmployeeScheduleWithOnlyWeekDayAsync(int employeeId);
}
