namespace DentallApp.Features.Employees.DTOs;

public class EmployeeInsertDto : UserInsertDto
{
    public int OfficeId { get; set; }
    public string PregradeUniversity { get; set; }
    public string PostgradeUniversity { get; set; }

    [Required]
    [MaxLength(NumberRoles.MaxRole)]
    [MinLength(NumberRoles.MinRole)]
    public IEnumerable<int> Roles { get; set; }
}
