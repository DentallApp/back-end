﻿namespace DentallApp.Features.Chatbot;

public interface IAppointmentBotService
{
    Task<List<AdaptiveChoice>> GetPatientsAsync(UserProfile userProfile);
    Task<List<AdaptiveChoice>> GetOfficesAsync();
    Task<List<AdaptiveChoice>> GetDentalServicesAsync();
    Task<List<AdaptiveChoice>> GetDentistsByOfficeIdAsync(int officeId);
    Task<Response<IEnumerable<AvailableTimeRangeDto>>> GetAvailableHoursAsync(AvailableTimeRangePostDto availableTimeRangeDto);
    Task<Response> CreateScheduledAppointmentAsync(AppointmentInsertDto appointment);
    Task<SpecificTreatmentRangeToPayDto> GetRangeToPayAsync(int dentalServiceId);
    Task<string> GetDentistScheduleAsync(int dentistId);
}