namespace DentallApp.Shared.Domain;

public class Role : BaseEntity
{
    public string Name { get; set; }
    public ICollection<UserRole> UserRoles { get; set; }
}
