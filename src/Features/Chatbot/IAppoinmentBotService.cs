namespace DentallApp.Features.Chatbot;

public interface IAppoinmentBotService
{
    Task<List<AdaptiveChoice>> GetPatientsAsync(UserProfile userProfile);
    Task<List<AdaptiveChoice>> GetOfficesAsync();
    Task<List<AdaptiveChoice>> GetDentalServicesAsync();
    Task<List<AdaptiveChoice>> GetDentistsByOfficeIdAsync(int officeId);
    Task<Response<IEnumerable<AvailableTimeRangeDto>>> GetAvailableHoursAsync(AvailableTimeRangePostDto availableTimeRangeDto);
    Task<Response> CreateScheduledAppoinmentAsync(AppoinmentInsertDto appoinment);
}
