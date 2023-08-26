namespace DentallApp.Features.Appointments;

public interface IAppointmentRepository : IRepository<Appointment>
{
    Task<AppointmentInfoDto> GetAppointmentInformationAsync(int id);
    Task<List<UnavailableTimeRangeDto>> GetUnavailableHoursAsync(int dentistId, DateTime appointmentDate);
}
