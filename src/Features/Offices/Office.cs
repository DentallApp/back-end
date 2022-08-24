namespace DentallApp.Features.Offices;

public class Office : ModelWithSoftDelete
{
    public string Name { get; set; }
    public string Address { get; set; }
    public ICollection<Employee> Employees { get; set; }
    public ICollection<Appoinment> Appoinments { get; set; }
    public ICollection<EmployeeSchedule> EmployeeSchedules { get; set; }
}
