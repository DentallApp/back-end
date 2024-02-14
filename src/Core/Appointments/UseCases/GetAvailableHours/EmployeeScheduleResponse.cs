namespace DentallApp.Core.Appointments.UseCases.GetAvailableHours;

public class EmployeeScheduleResponse : IEmployeeSchedule
{
    public int EmployeeScheduleId { get; init; }
    public bool IsEmployeeScheculeDeleted { get; init; }
    public int OfficeId { get; init; }
    public bool IsOfficeDeleted { get; init; }
    public bool IsOfficeScheduleDeleted { get; init; }
    public TimeSpan? MorningStartHour { get; set; }
    public TimeSpan? MorningEndHour { get; set; }
    public TimeSpan? AfternoonStartHour { get; set; }
    public TimeSpan? AfternoonEndHour { get; set; }
}
