namespace DentallApp.Domain.Entities;

public class Dependent : SoftDeleteEntity
{
    public int UserId { get; set; }
    public User User { get; set; }
    public int PersonId { get; set; }
    public Person Person { get; set; }
    public int KinshipId { get; set; }
    public Kinship Kinship { get; set; }
}
