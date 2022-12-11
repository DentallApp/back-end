namespace DentallApp.Features.Employees.DTOs;

public class EmployeeUpdateByAdminDto : EmployeeUpdateDto
{
    public int OfficeId { get; set; }
    public string Document { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    [Required]
    [MaxLength(NumberRoles.MaxRole)]
    [MinLength(NumberRoles.MinRole)]
    public List<int> Roles { get; set; }
    public bool IsDeleted { get; set; }
}
