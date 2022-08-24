namespace DentallApp.Features.Appoinments;

public class Appoinment : ModelBase
{
    public int UserId { get; set; }
    public User User { get; set; }
    public int PersonId { get; set; }
    public Person Person { get; set; }
    public int DentistId { get; set; }
    [ForeignKey("DentistId")]
    public Employee Employee { get; set; }
    public int AppoinmentStatusId { get; set; }
    public AppoinmentStatus AppoinmentStatus { get; set; }
    public int GeneralTreatmentId { get; set; }
    public GeneralTreatment GeneralTreatment { get; set; }
    public int OfficeId { get; set; }
    public Office Office { get; set; }
    [Column(TypeName = "Date")]
    public DateTime Date { get; set; }
    public TimeSpan StartHour { get; set; }
    public TimeSpan EndHour { get; set; }
    public bool IsCancelledByEmployee { get; set; }
}
