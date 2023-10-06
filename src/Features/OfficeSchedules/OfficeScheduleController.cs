using DentallApp.Features.OfficeSchedules.UseCases;

namespace DentallApp.Features.OfficeSchedules;

[Route("office-schedule")]
[ApiController]
public class OfficeScheduleController : ControllerBase
{
    /// <summary>
    /// Crea un nuevo horario para el consultorio.
    /// </summary>
    [AuthorizeByRole(RolesName.Admin, RolesName.Superadmin)]
    [HttpPost]
    public async Task<ActionResult<Response<InsertedIdDto>>> Create(
        [FromBody]CreateOfficeScheduleRequest request,
        [FromServices]CreateOfficeScheduleUseCase useCase)
    {
        var response = await useCase.ExecuteAsync(User, request);
        return response.Success ? CreatedAtAction(nameof(Create), response) : BadRequest(response);
    }

    /// <summary>
    /// Actualiza el horario de un consultorio.
    /// </summary>
    [AuthorizeByRole(RolesName.Admin, RolesName.Superadmin)]
    [HttpPut("{scheduleId}")]
    public async Task<ActionResult<Response>> Update(
        int scheduleId,
        [FromBody]UpdateOfficeScheduleRequest request,
        [FromServices]UpdateOfficeScheduleUseCase useCase)
    {
        var response = await useCase.ExecuteAsync(scheduleId, User, request);
        return response.Success ? Ok(response) : BadRequest(response);
    }

    /// <summary>
    /// Obtiene el horario de un consultorio activo o inactivo.
    /// </summary>
    [HttpGet("{officeId}")]
    public async Task<IEnumerable<GetSchedulesByOfficeIdResponse>> GetByOfficeId(
        int officeId,
        [FromServices]GetSchedulesByOfficeIdUseCase useCase)
    {
        return await useCase.ExecuteAsync(officeId);
    }
}
