namespace DentallApp.Features.FavoriteDentists;

public class FavoriteDentist : ModelBase
{
    public int UserId { get; set; }
    public User User { get; set; }
    public int DentistId { get; set; }
    [ForeignKey("DentistId")]
    public Employee Employee { get; set; }
}
