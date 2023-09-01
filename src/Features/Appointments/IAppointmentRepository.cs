namespace DentallApp.Features.Appointments;

public interface IAppointmentRepository
{
    Task<List<UnavailableTimeRangeResponse>> GetUnavailableHoursAsync(int dentistId, DateTime appointmentDate);
}
