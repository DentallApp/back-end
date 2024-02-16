namespace DentallApp.Shared.Entities;
    
public class Employee : SoftDeleteEntity, IAuditableEntity
{
    public int UserId { get; set; }
    public User User { get; set; }
    public int PersonId { get; set; }
    public Person Person { get; set; }
    public int OfficeId { get; set; }
    public Office Office { get; set; }
    public string PregradeUniversity { get; set; }
    public string PostgradeUniversity { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public ICollection<EmployeeSchedule> EmployeeSchedules { get; set; }
    public List<EmployeeSpecialty> EmployeeSpecialties { get; set; }

    public bool IsSuperAdmin()
    {
        return User.UserRoles.Any(userRole => userRole.RoleId == (int)Role.Predefined.Superadmin);
    }
}
