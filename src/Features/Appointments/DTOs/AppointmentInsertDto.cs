namespace DentallApp.Features.Appointments.DTOs;

public class AppointmentInsertDto
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
    public SpecificTreatmentRangeToPayDto RangeToPay { get; set; }
}
