namespace DentallApp.Features.EmployeeSchedules.DTOs;

public class EmployeeScheduleDto
{
    public TimeSpan? MorningStartHour { get; set; }
    public TimeSpan? MorningEndHour { get; set; }
    public TimeSpan? AfternoonStartHour { get; set; }
    public TimeSpan? AfternoonEndHour { get; set; }
}
