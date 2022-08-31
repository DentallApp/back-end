namespace DentallApp.Features.EmployeeSchedules.DTOs;

public class ScheduleDto
{
    public int WeekDayId { get; set; }
    public string WeekDayName { get; set; }
    public string MorningStartHour { get; set; }
    public string MorningEndHour { get; set; }
    public string AfternoonStartHour { get; set; }
    public string AfternoonEndHour { get; set; }
}
