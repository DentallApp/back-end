namespace DentallApp.Features.OfficeSchedules.DTOs;

public class OfficeScheduleGetDto
{
    public int ScheduleId { get; set; }
    public int WeekDayId { get; set; }
    public string WeekDayName { get; set; }
    public string Status { get; set; }
    public bool IsDeleted { get; set; }
    public string StartHour { get; set; }
    public string EndHour { get; set; }
}
