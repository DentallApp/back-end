namespace DentallApp.Features.EmployeeSchedules.DTOs;

public class EmployeeScheduleByWeekDayDto : EmployeeScheduleDto
{
    public int EmployeeScheduleId { get; set; }
    public bool IsEmployeeScheculeDeleted { get; set; }
    public int OfficeId { get; set; }
    public bool IsOfficeDeleted { get; set; }
    public bool IsOfficeScheduleDeleted { get; set; }
}
