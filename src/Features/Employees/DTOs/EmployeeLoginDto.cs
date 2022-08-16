namespace DentallApp.Features.Employees.DTOs;

public class EmployeeLoginDto : FullEmployeeProfileDto
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}
