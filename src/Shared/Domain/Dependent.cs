namespace DentallApp.Shared.Domain;

public class Dependent : SoftDeleteEntity, IAuditableEntity
{
    public int UserId { get; set; }
    public User User { get; set; }
    public int PersonId { get; set; }
    public Person Person { get; set; }
    public int KinshipId { get; set; }
    public Kinship Kinship { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
