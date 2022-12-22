namespace DentallApp.Entities;

public class UserRole : EntityBase, IIntermediateEntity
{
    public int UserId { get; set; }
    public User User { get; set; }
    public int RoleId { get; set; }
    public Role Role { get; set; }

    /// <inheritdoc />
    [NotMapped]
    public int PrimaryForeignKey 
    { 
        get => UserId; 
        set => UserId = value; 
    }

    /// <inheritdoc /> 
    [NotMapped]
    public int SecondaryForeignKey 
    { 
        get => RoleId; 
        set => RoleId = value; 
    }
}
