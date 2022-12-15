namespace DentallApp.Models;
using AppointmentType = AppointmentStatusId;

public class Appointment : ModelBase
{
    /// <summary>
    /// El ID del usuario que agendó la cita.
    /// </summary>
    public int UserId { get; set; }
    /// <summary>
    /// Representa el usuario que agendó la cita.
    /// </summary>
    public User User { get; set; }

    /// <summary>
    /// El ID de la persona que recibirá la atención médica.
    /// </summary>
    public int PersonId { get; set; }
    /// <summary>
    /// Representa la persona que recibirá la atención médica.
    /// </summary>
    public Person Person { get; set; }
    public int DentistId { get; set; }
    [ForeignKey("DentistId")]
    public Employee Employee { get; set; }
    public int AppointmentStatusId { get; set; } = AppointmentType.Scheduled;
    public AppointmentStatus AppointmentStatus { get; set; }
    public int GeneralTreatmentId { get; set; }
    public GeneralTreatment GeneralTreatment { get; set; }
    public int OfficeId { get; set; }
    public Office Office { get; set; }
    [Column(TypeName = "Date")]
    public DateTime Date { get; set; }
    public TimeSpan StartHour { get; set; }
    public TimeSpan EndHour { get; set; }

    /// <summary>
    /// Comprueba sí la cita fue cancelada por el empleado.
    /// </summary>
    /// <remarks>
    /// Esta propiedad permite que la fecha y hora de la cita cancelada por el empleado 
    /// no se muestre como disponible cuando el usuario básico agende una nueva cita.
    /// </remarks>
    public bool IsCancelledByEmployee { get; set; }
}
