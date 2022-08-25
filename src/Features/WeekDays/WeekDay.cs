namespace DentallApp.Features.WeekDays;

public class WeekDay : ModelBase
{
    public string Name { get; set; }
    public ICollection<EmployeeSchedule> EmployeeSchedules { get; set; }
    public ICollection<OfficeSchedule> OfficeSchedules { get; set; }
}
