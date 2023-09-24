namespace DentallApp.Domain.Entities;

public class User : EntityBase, IAuditableEntity
{
    [Column("username")]
    public string UserName { get; set; }
    public string Password { get; set; }
    public string RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiry { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int PersonId { get; set; }
    public Person Person { get; set; }
    public Employee Employee { get; set; }
    public List<UserRole> UserRoles { get; set; }
    public ICollection<Dependent> Dependents { get; set; }
    public ICollection<Appointment> Appointments { get; set; }
    public ICollection<FavoriteDentist> FavoriteDentists { get; set; }

    public bool IsUnverified()
    {
        return UserRoles.First().RoleId == RolesId.Unverified;
    }

    public bool IsVerified()
    {
        return !IsUnverified();
    }

    public bool IsBasicUser()
    {
        return UserRoles.Any(userRole => userRole.RoleId == RolesId.BasicUser);
    }
}
