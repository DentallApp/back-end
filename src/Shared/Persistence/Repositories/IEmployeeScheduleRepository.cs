namespace DentallApp.Shared.Persistence.Repositories;

public interface IEmployeeScheduleRepository
{
    Task<EmployeeScheduleResponse> GetByWeekDayId(int employeeId, int weekDayId);
    Task<IEnumerable<WeekDayResponse>> GetOnlyWeekDays(int employeeId);
}
