namespace DentallApp.Domain.Entities;

public class FavoriteDentist : EntityBase
{
    public int UserId { get; set; }
    public User User { get; set; }
    public int DentistId { get; set; }
    [ForeignKey("DentistId")]
    public Employee Employee { get; set; }
}
