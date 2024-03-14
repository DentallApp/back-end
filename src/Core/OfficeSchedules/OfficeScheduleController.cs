using DentallApp.Core.OfficeSchedules.UseCases;

namespace DentallApp.Core.OfficeSchedules;

[Route("office-schedule")]
[ApiController]
public class OfficeScheduleController
{
    /// <summary>
    /// Creates a new schedule for the dental office.
    /// </summary>
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType<Result>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<Result>(StatusCodes.Status403Forbidden)]
    [AuthorizeByRole(RoleName.Admin, RoleName.Superadmin)]
    [HttpPost]
    public async Task<Result<CreatedId>> Create(
        [FromBody]CreateOfficeScheduleRequest request,
        CreateOfficeScheduleUseCase useCase)
        => await useCase.ExecuteAsync(request);

    /// <summary>
    /// Updates the schedule of a dental office.
    /// </summary>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<Result>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<Result>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<Result>(StatusCodes.Status403Forbidden)]
    [AuthorizeByRole(RoleName.Admin, RoleName.Superadmin)]
    [HttpPut("{scheduleId}")]
    public async Task<Result> Update(
        int scheduleId,
        [FromBody]UpdateOfficeScheduleRequest request,
        UpdateOfficeScheduleUseCase useCase)
        => await useCase.ExecuteAsync(scheduleId, request);

    /// <summary>
    /// Gets the schedules of an active or inactive office.
    /// </summary>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet("{officeId}")]
    public async Task<IEnumerable<GetSchedulesByOfficeIdResponse>> GetByOfficeId(
        int officeId,
        GetSchedulesByOfficeIdUseCase useCase)
        => await useCase.ExecuteAsync(officeId);
}
