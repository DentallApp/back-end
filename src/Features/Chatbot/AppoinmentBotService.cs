﻿namespace DentallApp.Features.Chatbot;

public class AppoinmentBotService : IAppoinmentBotService
{
    private readonly IServiceProvider _serviceProvider;

    public AppoinmentBotService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<List<AdaptiveChoice>> GetDentalServicesAsync()
    {
		using var scope = _serviceProvider.CreateScope();
        var repository = scope.ServiceProvider.GetRequiredService<IBotQueryRepository>();
        return await repository.GetDentalServicesAsync();
    }

    public async Task<List<AdaptiveChoice>> GetDentistsByOfficeIdAsync(int officeId)
    {
		using var scope = _serviceProvider.CreateScope();
        var repository = scope.ServiceProvider.GetRequiredService<IBotQueryRepository>();
        return await repository.GetDentistsByOfficeIdAsync(officeId);
    }

    public async Task<List<AdaptiveChoice>> GetOfficesAsync()
    {
		using var scope = _serviceProvider.CreateScope();
        var repository = scope.ServiceProvider.GetRequiredService<IBotQueryRepository>();
        return await repository.GetOfficesAsync();
    }

    public async Task<List<AdaptiveChoice>> GetPatientsAsync(UserProfile userProfile)
    {
		using var scope = _serviceProvider.CreateScope();
        var repository = scope.ServiceProvider.GetRequiredService<IBotQueryRepository>();
        return await repository.GetPatientsAsync(userProfile);
    }

    public async Task<Response<IEnumerable<AvailableTimeRangeDto>>> GetAvailableHoursAsync(AvailableTimeRangePostDto availableTimeRangeDto)
    {
        using var scope = _serviceProvider.CreateScope();
        var availabilityService = scope.ServiceProvider.GetRequiredService<IAvailabilityService>();
        return await availabilityService.GetAvailableHoursAsync(availableTimeRangeDto);
    }

    public async Task<Response> CreateScheduledAppoinmentAsync(AppoinmentInsertDto appoinment)
    {
        using var scope = _serviceProvider.CreateScope();
        var appoinmentService = scope.ServiceProvider.GetRequiredService<IAppoinmentService>();
        return await appoinmentService.CreateAppoinmentAsync(appoinment);
    }

    public async Task<SpecificTreatmentRangeToPayDto> GetRangeToPayAsync(int dentalServiceId)
    {
        using var scope = _serviceProvider.CreateScope();
        var repository = scope.ServiceProvider.GetRequiredService<ISpecificTreatmentRepository>();
        return await repository.GetTreatmentWithRangeToPayAsync(dentalServiceId);
    }
}
