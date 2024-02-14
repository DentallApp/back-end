using DentallApp.Core.AppointmentStatuses.UseCases;

namespace DentallApp.Core.AppointmentStatuses;

[Route("appointment-status")]
[ApiController]
public class AppointmentStatusController
{
    [HttpGet]
    public async Task<IEnumerable<GetAppointmentStatusesResponse>> GetAll(
        GetAppointmentStatusesUseCase useCase)
        => await useCase.ExecuteAsync();
}
