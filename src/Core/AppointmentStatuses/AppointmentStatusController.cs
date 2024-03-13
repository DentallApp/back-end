using DentallApp.Core.AppointmentStatuses.UseCases;

namespace DentallApp.Core.AppointmentStatuses;

[Route("appointment-status")]
[ApiController]
public class AppointmentStatusController
{
    /// <summary>
    /// Gets the appointment statuses.
    /// </summary>
    [HttpGet]
    public async Task<IEnumerable<GetAppointmentStatusesResponse>> GetAll(
        GetAppointmentStatusesUseCase useCase)
        => await useCase.ExecuteAsync();
}
