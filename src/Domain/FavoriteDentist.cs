namespace DentallApp.Domain;

public class FavoriteDentist : BaseEntity, IAuditableEntity
{
    public int UserId { get; set; }
    public User User { get; set; }
    public int DentistId { get; set; }
    [ForeignKey("DentistId")]
    public Employee Employee { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
