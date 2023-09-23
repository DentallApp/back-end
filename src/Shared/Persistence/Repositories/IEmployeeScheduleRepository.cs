namespace DentallApp.Shared.Persistence.Repositories;

public interface IEmployeeScheduleRepository
{
    Task<EmployeeScheduleResponse> GetByWeekDayIdAsync(int employeeId, int weekDayId);
    Task<IEnumerable<WeekDay>> GetOnlyWeekDaysAsync(int employeeId);
}
