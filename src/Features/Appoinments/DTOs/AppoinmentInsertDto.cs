namespace DentallApp.Features.Appoinments.DTOs;

public class AppoinmentInsertDto
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
    public DateTime AppoinmentDate { get; set; }
    public TimeSpan StartHour { get; set; }
    public TimeSpan EndHour { get; set; }
}
