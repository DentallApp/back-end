namespace DentallApp.Features.OfficeSchedules.DTOs;

public class OfficeScheduleDto
{
    public int WeekDayId { get; set; }
    public string WeekDayName { get; set; }
    public string StartHour { get; set; }
    public string EndHour { get; set; }
}
