using DentallApp.Features.EmployeeSchedules.UseCases;

namespace DentallApp.Features.EmployeeSchedules;

[AuthorizeByRole(RoleName.Secretary, RoleName.Admin)]
[Route("employee-schedule")]
[ApiController]
public class EmployeeScheduleController
{
    /// <summary>
    /// Crea un nuevo horario para el empleado.
    /// </summary>
    [HttpPost]
    public async Task<Result<CreatedId>> Create(
        [FromBody]CreateEmployeeScheduleRequest request,
        CreateEmployeeScheduleUseCase useCase)
        => await useCase.ExecuteAsync(request);

    /// <summary>
    /// Actualiza el horario de un empleado.
    /// </summary>
    [HttpPut("{id}")]
    public async Task<Result> Update(
        int id, 
        [FromBody]UpdateEmployeeScheduleRequest request,
        UpdateEmployeeScheduleUseCase useCase)
        => await useCase.ExecuteAsync(id, request);

    /// <summary>
    /// Obtiene el horario de un empleado.
    /// </summary>
    [HttpGet("{employeeId}")]
    public async Task<IEnumerable<GetSchedulesByEmployeeIdResponse>> GetByEmployeeId(
        int employeeId,
        GetSchedulesByEmployeeIdUseCase useCase)
        => await useCase.ExecuteAsync(employeeId);
}
