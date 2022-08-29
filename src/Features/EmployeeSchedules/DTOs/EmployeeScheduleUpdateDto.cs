namespace DentallApp.Features.EmployeeSchedules.DTOs;

public class EmployeeScheduleUpdateDto : EmployeeScheduleDto
{
    public int WeekDayId { get; set; }
    public bool IsDeleted { get; set; }
}
