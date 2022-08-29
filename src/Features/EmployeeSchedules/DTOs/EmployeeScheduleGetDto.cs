namespace DentallApp.Features.EmployeeSchedules.DTOs;

public class EmployeeScheduleGetDto
{
    public int ScheduleId { get; set; }
    public int WeekDayId { get; set; }
    public string WeekDayName { get; set; }
    public string Status { get; set; }
    public bool IsDeleted { get; set; }
    public string MorningStartHour { get; set; }
    public string MorningEndHour { get; set; }
    public string AfternoonStartHour { get; set; }
    public string AfternoonEndHour { get; set; }
}
