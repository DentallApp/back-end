namespace DentallApp.Features.OfficeSchedules;

public interface IOfficeScheduleService
{
    Task<Response> CreateOfficeScheduleAsync(ClaimsPrincipal currentEmployee, OfficeScheduleInsertDto officeScheduleInsertDto);
    Task<Response> UpdateOfficeScheduleAsync(int scheduleId, ClaimsPrincipal currentEmployee, OfficeScheduleUpdateDto officeScheduleUpdateDto);
    Task<IEnumerable<OfficeScheduleGetAllDto>> GetAllOfficeSchedulesAsync();
    Task<IEnumerable<OfficeScheduleShowDto>> GetHomePageSchedulesAsync();
}
