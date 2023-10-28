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
    public async Task<Result<CreatedId>> Create(
        [FromBody]CreateEmployeeScheduleRequest request,
        [FromServices]CreateEmployeeScheduleUseCase useCase)
    {
        return await useCase.ExecuteAsync(request);
    }

    /// <summary>
    /// Actualiza el horario de un empleado.
    /// </summary>
    [HttpPut("{id}")]
    public async Task<Result> Update(
        int id, 
        [FromBody]UpdateEmployeeScheduleRequest request,
        [FromServices]UpdateEmployeeScheduleUseCase useCase)
    {
        return await useCase.ExecuteAsync(id, request);
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
