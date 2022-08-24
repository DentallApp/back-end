namespace DentallApp.Features.Users;

public class User : ModelBase
{
    [Column("username")]
    public string UserName { get; set; }
    public string Password { get; set; }
    public string RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiry { get; set; }
    public int PersonId { get; set; }
    public Person Person { get; set; }
    public Employee Employee { get; set; }
    public ICollection<UserRole> UserRoles { get; set; }
    public ICollection<Dependent> Dependents { get; set; }
    public ICollection<Appoinment> Appoinments { get; set; }
    public ICollection<FavoriteDentist> FavoriteDentists { get; set; }
}
