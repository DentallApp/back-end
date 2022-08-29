namespace DentallApp.Features.EmployeeSchedules;

public interface IEmployeeScheduleService
{
    Task<Response> CreateEmployeeScheduleAsync(EmployeeScheduleInsertDto employeeScheduleDto);
    Task<Response> UpdateEmployeeScheduleAsync(int scheduleId, EmployeeScheduleUpdateDto employeeScheduleDto);
    Task<IEnumerable<EmployeeScheduleGetDto>> GetEmployeeScheduleByEmployeeIdAsync(int employeeId);
}
