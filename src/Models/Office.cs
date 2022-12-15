﻿namespace DentallApp.Models;

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
    public ICollection<Appointment> Appointments { get; set; }
    public ICollection<OfficeSchedule> OfficeSchedules { get; set; }
}