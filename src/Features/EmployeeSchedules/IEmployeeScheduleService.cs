namespace DentallApp.Features.EmployeeSchedules;

public interface IEmployeeScheduleService
{
    Task<Response<DtoBase>> CreateEmployeeScheduleAsync(EmployeeScheduleInsertDto employeeScheduleDto);
    Task<Response> UpdateEmployeeScheduleAsync(int scheduleId, EmployeeScheduleUpdateDto employeeScheduleDto);
}
