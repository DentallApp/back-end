namespace DentallApp.Features.EmployeeSchedules.DTOs;

public interface IEmployeeScheduleDto
{
    TimeSpan? MorningStartHour { get; set; }
    TimeSpan? MorningEndHour { get; set; }
    TimeSpan? AfternoonStartHour { get; set; }
    TimeSpan? AfternoonEndHour { get; set; }
}
