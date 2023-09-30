namespace DentallApp.Shared.Domain;

public class Person : BaseEntity, IAuditableEntity
{
    public string Document { get; set; }
    public string Names { get; set; }
    public string LastNames { get; set; }
    public string CellPhone { get; set; }
    public string Email { get; set; }
    [Column(TypeName = "Date")]
    public DateTime? DateBirth { get; set; }
    public int? GenderId { get; set; }
    public Gender Gender { get; set; }
    public User User { get; set; }
    public Employee Employee { get; set; }
    public Dependent Dependent { get; set; }
    public ICollection<Appointment> Appointments { get; set; }

    [Decompile]
    [NotMapped]
    public string FullName => Names + " " + LastNames;
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
