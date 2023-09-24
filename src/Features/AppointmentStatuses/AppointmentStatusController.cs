using DentallApp.Features.AppointmentStatuses.UseCases;

namespace DentallApp.Features.AppointmentStatuses;

[Route("appointment-status")]
[ApiController]
public class AppointmentStatusController : ControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<GetAppointmentStatusesResponse>> GetAll(
        [FromServices]GetAppointmentStatusesUseCase useCase)
    {
        return await useCase.ExecuteAsync();
    }
}
