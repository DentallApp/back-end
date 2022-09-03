namespace DentallApp.Features.OfficeSchedules.DTOs;

public class OfficeScheduleShowDto
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string ContactNumber { get; set; }
    public IEnumerable<OfficeScheduleDto> Schedules { get; set; }
}
