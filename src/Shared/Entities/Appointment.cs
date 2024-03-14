namespace DentallApp.Shared.Entities;

public class Appointment : BaseEntity, IAuditableEntity
{
    /// <summary>
    /// The ID of the user who scheduled the appointment.
    /// </summary>
    public int UserId { get; set; }
    /// <summary>
    /// Represents the user who schedules the appointment.
    /// </summary>
    public User User { get; set; }

    /// <summary>
    /// The ID of the person who will receive medical care.
    /// </summary>
    public int PersonId { get; set; }
    /// <summary>
    /// Represents the person who will receive medical care.
    /// </summary>
    public Person Person { get; set; }
    public int DentistId { get; set; }
    [ForeignKey("DentistId")]
    public Employee Employee { get; set; }
    public int AppointmentStatusId { get; set; } = (int)AppointmentStatus.Predefined.Scheduled;
    public AppointmentStatus AppointmentStatus { get; set; }
    public int GeneralTreatmentId { get; set; }
    public GeneralTreatment GeneralTreatment { get; set; }
    public int OfficeId { get; set; }
    public Office Office { get; set; }
    [Column(TypeName = "Date")]
    public DateTime Date { get; set; }
    public TimeSpan StartHour { get; set; }
    public TimeSpan EndHour { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Checks if the appointment was canceled by the employee.
    /// </summary>
    /// <remarks>
    /// This property allows that the date and time of the appointment canceled by the employee 
    /// to not be shown as available when the basic user schedules a new appointment.
    /// </remarks>
    public bool IsCancelledByEmployee { get; set; }

    /// <summary>
    /// Checks if the medical appointment has not been cancelled.
    /// </summary>
    /// <returns>
    /// <c>true</c> if the appointment is not cancelled, otherwise it returns <c>false</c>.
    /// </returns>
    [Decompile]
    public bool IsNotCanceled()
    {
        return AppointmentStatusId != (int)AppointmentStatus.Predefined.Canceled;
    }
}
