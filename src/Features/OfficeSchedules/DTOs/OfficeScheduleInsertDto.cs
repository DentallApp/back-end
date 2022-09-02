namespace DentallApp.Features.OfficeSchedules.DTOs;

public class OfficeScheduleInsertDto
{
    public int WeekDayId { get; set; }
    public int OfficeId { get; set; }
    public TimeSpan StartHour { get; set; }
    public TimeSpan EndHour { get; set; }
}
