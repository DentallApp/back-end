namespace DentallApp.Features.OfficeSchedules.DTOs;

public class OfficeScheduleUpdateDto
{
    public int WeekDayId { get; set; }
    public TimeSpan StartHour { get; set; }
    public TimeSpan EndHour { get; set; }
    public bool IsDeleted { get; set; }
}
