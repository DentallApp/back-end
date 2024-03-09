using DentallApp.Core.OfficeSchedules.UseCases;

namespace DentallApp.Core.OfficeSchedules;

[Route("office-schedule")]
[ApiController]
public class OfficeScheduleController
{
    /// <summary>
    /// Crea un nuevo horario para el consultorio.
    /// </summary>
    [AuthorizeByRole(RoleName.Admin, RoleName.Superadmin)]
    [HttpPost]
    public async Task<Result<CreatedId>> Create(
        [FromBody]CreateOfficeScheduleRequest request,
        CreateOfficeScheduleUseCase useCase)
        => await useCase.ExecuteAsync(request);

    /// <summary>
    /// Actualiza el horario de un consultorio.
    /// </summary>
    [AuthorizeByRole(RoleName.Admin, RoleName.Superadmin)]
    [HttpPut("{scheduleId}")]
    public async Task<Result> Update(
        int scheduleId,
        [FromBody]UpdateOfficeScheduleRequest request,
        UpdateOfficeScheduleUseCase useCase)
        => await useCase.ExecuteAsync(scheduleId, request);

    /// <summary>
    /// Obtiene el horario de un consultorio activo o inactivo.
    /// </summary>
    [HttpGet("{officeId}")]
    public async Task<IEnumerable<GetSchedulesByOfficeIdResponse>> GetByOfficeId(
        int officeId,
        GetSchedulesByOfficeIdUseCase useCase)
        => await useCase.ExecuteAsync(officeId);
}
