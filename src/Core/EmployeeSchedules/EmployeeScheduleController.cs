using DentallApp.Core.EmployeeSchedules.UseCases;

namespace DentallApp.Core.EmployeeSchedules;

[AuthorizeByRole(RoleName.Secretary, RoleName.Admin)]
[Route("employee-schedule")]
[ApiController]
public class EmployeeScheduleController
{
    /// <summary>
    /// Creates a new schedule for the employee.
    /// </summary>
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType<Result>(StatusCodes.Status400BadRequest)]
    [HttpPost]
    public async Task<Result<CreatedId>> Create(
        [FromBody]CreateEmployeeScheduleRequest request,
        CreateEmployeeScheduleUseCase useCase)
        => await useCase.ExecuteAsync(request);

    /// <summary>
    /// Updates the schedule of an employee.
    /// </summary>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<Result>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<Result>(StatusCodes.Status404NotFound)]
    [HttpPut("{id}")]
    public async Task<Result> Update(
        int id, 
        [FromBody]UpdateEmployeeScheduleRequest request,
        UpdateEmployeeScheduleUseCase useCase)
        => await useCase.ExecuteAsync(id, request);

    /// <summary>
    /// Gets the schedules of an employee.
    /// </summary>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet("{employeeId}")]
    public async Task<IEnumerable<GetSchedulesByEmployeeIdResponse>> GetByEmployeeId(
        int employeeId,
        GetSchedulesByEmployeeIdUseCase useCase)
        => await useCase.ExecuteAsync(employeeId);
}
