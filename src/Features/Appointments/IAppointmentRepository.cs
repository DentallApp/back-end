namespace DentallApp.Features.Appointments;

public interface IAppointmentRepository
{
    Task<List<UnavailableTimeRangeDto>> GetUnavailableHoursAsync(int dentistId, DateTime appointmentDate);
}
