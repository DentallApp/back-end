﻿namespace DentallApp.Features.Employees.DTOs;

public class EmployeeUpdateByAdminDto : EmployeeUpdateDto
{
    public int OfficeId { get; set; }
    public string Document { get; set; }
    public string Email { get; set; }
    public IEnumerable<int> Roles { get; set; }
}
