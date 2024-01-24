using DentallApp.Features.AppointmentStatuses.UseCases;

namespace DentallApp.Features.AppointmentStatuses;

[Route("appointment-status")]
[ApiController]
public class AppointmentStatusController
{
    [HttpGet]
    public async Task<IEnumerable<GetAppointmentStatusesResponse>> GetAll(
        GetAppointmentStatusesUseCase useCase)
        => await useCase.ExecuteAsync();
}
