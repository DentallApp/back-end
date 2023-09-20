namespace DentallApp.Domain.Entities;

public class WeekDay : EntityBase
{
    public string Name { get; set; }
    public ICollection<EmployeeSchedule> EmployeeSchedules { get; set; }
    public ICollection<OfficeSchedule> OfficeSchedules { get; set; }
}
