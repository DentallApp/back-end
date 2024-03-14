namespace DentallApp.Shared.Interfaces.Appointments;

public class CreateAppointmentRequest
{
    /// <summary>
    /// The ID of the user who scheduled the appointment.
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// The ID of the person who will receive medical care.
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
