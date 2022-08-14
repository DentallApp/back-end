namespace DentallApp.Features.Employees.DTOs;

public class EmployeeUpdateDto : UserUpdateDto
{
    public string PregradeUniversity { get; set; }
    public string PostgradeUniversity { get; set; }
}
