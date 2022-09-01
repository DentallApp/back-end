namespace DentallApp.Features.EmployeeSchedules.DTOs;

public class EmployeeScheduleGetAllDto
{
    public string FullName { get; set; }
    public bool IsEmployeeDeleted { get; set; }
    public IEnumerable<ScheduleDto> Schedules { get; set; }
}
