namespace DentallApp.Shared.Appointments;

public class CreateAppointmentRequest
{
    /// <summary>
    /// El ID del usuario que agendó la cita.
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// El ID de la persona que recibirá la atención médica.
    /// </summary>
    public int PersonId { get; set; }
    public int DentistId { get; set; }
    public int GeneralTreatmentId { get; set; }
    public int OfficeId { get; set; }
    public DateTime AppointmentDate { get; set; }
    public TimeSpan StartHour { get; set; }
    public TimeSpan EndHour { get; set; }

    [JsonIgnore]
    public PayRange RangeToPay { get; set; }

    public Appointment MapToAppointment() => new()
    {
        UserId             = UserId,
        PersonId           = PersonId,
        DentistId          = DentistId,
        OfficeId           = OfficeId,
        Date               = AppointmentDate,
        StartHour          = StartHour,
        EndHour            = EndHour,
        GeneralTreatmentId = GeneralTreatmentId
    };
}

/// <summary>
/// This interface is used so that the chatbot is not directly coupled to the <c>Appointments</c> module.
/// </summary>
public interface ICreateAppointmentUseCase
{
    Task<Result<CreatedId>> ExecuteAsync(CreateAppointmentRequest request);
}
