using DentallApp.Features.EmployeeSchedules.UseCases;

namespace DentallApp.Features.EmployeeSchedules;

[AuthorizeByRole(RolesName.Secretary, RolesName.Admin)]
[Route("employee-schedule")]
[ApiController]
public class EmployeeScheduleController : ControllerBase
{
    /// <summary>
    /// Crea un nuevo horario para el empleado.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<Response<InsertedIdDto>>> Create(
        [FromBody]CreateEmployeeScheduleRequest request,
        [FromServices]CreateEmployeeScheduleUseCase useCase)
    {
        var response = await useCase.ExecuteAsync(request);
        return response.Success ? CreatedAtAction(nameof(Create), response) : BadRequest(response);
    }

    /// <summary>
    /// Actualiza el horario de un empleado.
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<Response>> Update(
        int id, 
        [FromBody]UpdateEmployeeScheduleRequest request,
        [FromServices]UpdateEmployeeScheduleUseCase useCase)
    {
        var response = await useCase.ExecuteAsync(id, request);
        return response.Success ? Ok(response) : BadRequest(response);
    }

    /// <summary>
    /// Obtiene el horario de un empleado.
    /// </summary>
    [HttpGet("{employeeId}")]
    public async Task<IEnumerable<GetSchedulesByEmployeeIdResponse>> GetByEmployeeId(
        int employeeId,
        [FromServices]GetSchedulesByEmployeeIdUseCase useCase)
    { 
        return await useCase.ExecuteAsync(employeeId);
    }
}
