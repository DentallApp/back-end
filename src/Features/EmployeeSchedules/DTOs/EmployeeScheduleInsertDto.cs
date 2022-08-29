namespace DentallApp.Features.EmployeeSchedules.DTOs;

public class EmployeeScheduleInsertDto : EmployeeScheduleDto
{
    public int EmployeeId { get; set; }
    public int WeekDayId { get; set; }
}
