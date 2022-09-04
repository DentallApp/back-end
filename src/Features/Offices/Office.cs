namespace DentallApp.Features.Offices;

public class Office : ModelWithSoftDelete
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string ContactNumber { get; set; }
    public bool IsEnabledEmployeeAccounts { get; set; } = true;
    public bool IsCheckboxTicked { get; set; } = true;
    [NotMapped]
    public bool IsDisabledEmployeeAccounts => !IsEnabledEmployeeAccounts;
    public ICollection<Employee> Employees { get; set; }
    public ICollection<Appoinment> Appoinments { get; set; }
    public ICollection<OfficeSchedule> OfficeSchedules { get; set; }
}
