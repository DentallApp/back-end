namespace DentallApp.Features.EmployeeSchedules.DTOs;

public class EmployeeScheduleGetAllDto
{
    public string FullName { get; set; }
    public bool EmployeeStatus { get; set; }
    public IEnumerable<ScheduleDto> Schedules { get; set; }
}
