namespace DentallApp.Features.ChatBot;

public interface IAppointmentBotService
{
    Task<List<AdaptiveChoice>> GetPatientsAsync(AuthenticatedUser user);
    Task<List<AdaptiveChoice>> GetOfficesAsync();
    Task<List<AdaptiveChoice>> GetDentalServicesAsync();
    Task<List<AdaptiveChoice>> GetDentistsAsync(int officeId, int specialtyId);
    Task<Response<IEnumerable<AvailableTimeRangeResponse>>> GetAvailableHoursAsync(AvailableTimeRangeRequest request);
    Task<Response<InsertedIdDto>> CreateScheduledAppointmentAsync(CreateAppointmentRequest appointment);
    Task<PayRange> GetRangeToPayAsync(int dentalServiceId);
    Task<string> GetDentistScheduleAsync(int dentistId);
}
