using DentallApp.Features.Appointments.UseCases;

namespace DentallApp.Features.Chatbot;

public interface IAppointmentBotService
{
    Task<List<AdaptiveChoice>> GetPatientsAsync(UserProfile userProfile);
    Task<List<AdaptiveChoice>> GetOfficesAsync();
    Task<List<AdaptiveChoice>> GetDentalServicesAsync();
    Task<List<AdaptiveChoice>> GetDentistsAsync(int officeId, int specialtyId);
    Task<Response<IEnumerable<AvailableTimeRangeDto>>> GetAvailableHoursAsync(AvailableTimeRangePostDto availableTimeRangeDto);
    Task<Response<InsertedIdDto>> CreateScheduledAppointmentAsync(CreateAppointmentRequest appointment);
    Task<SpecificTreatmentRangeToPayDto> GetRangeToPayAsync(int dentalServiceId);
    Task<string> GetDentistScheduleAsync(int dentistId);
}
