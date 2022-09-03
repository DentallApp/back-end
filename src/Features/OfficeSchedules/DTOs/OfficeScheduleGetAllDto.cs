namespace DentallApp.Features.OfficeSchedules.DTOs;

public class OfficeScheduleGetAllDto
{
    public string Name { get; set; }
    public bool IsOfficeDeleted { get; set; }
    public List<OfficeScheduleDto> Schedules { get; set; }
}
