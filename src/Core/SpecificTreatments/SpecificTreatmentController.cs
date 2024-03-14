using DentallApp.Core.SpecificTreatments.UseCases;

namespace DentallApp.Core.SpecificTreatments;

[Route("specific-treatment")]
[ApiController]
public class SpecificTreatmentController
{
    /// <summary>
    /// Creates a new specific treatment.
    /// </summary>
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType<Result>(StatusCodes.Status400BadRequest)]
    [AuthorizeByRole(RoleName.Superadmin)]
    [HttpPost]
    public async Task<Result<CreatedId>> Create(
        [FromBody]CreateSpecificTreatmentRequest request,
        CreateSpecificTreatmentUseCase useCase)
        => await useCase.ExecuteAsync(request);

    /// <summary>
    /// Updates a specific treatment by its ID.
    /// </summary>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<Result>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<Result>(StatusCodes.Status404NotFound)]
    [AuthorizeByRole(RoleName.Superadmin)]
    [HttpPut("{id}")]
    public async Task<Result> Update(
        int id, 
        [FromBody]UpdateSpecificTreatmentRequest request,
        UpdateSpecificTreatmentUseCase useCase)
        => await useCase.ExecuteAsync(id, request);

    /// <summary>
    /// Deletes a specific treatment by its ID.
    /// </summary>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<Result>(StatusCodes.Status404NotFound)]
    [AuthorizeByRole(RoleName.Superadmin)]
    [HttpDelete("{id}")]
    public async Task<Result> Delete(
        int id,
        DeleteSpecificTreatmentUseCase useCase)
        => await useCase.ExecuteAsync(id);

    /// <summary>
    /// Gets the specific treatments using the ID of a general treatment.
    /// </summary>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [AuthorizeByRole(RoleName.BasicUser, RoleName.Superadmin)]
    [HttpGet("{generalTreatmentId}")]
    public async Task<IEnumerable<GetTreatmentsByGeneralTreatmentIdResponse>> GetByGeneralTreatmentId(
        int generalTreatmentId,
        GetTreatmentsByGeneralTreatmentIdUseCase useCase)
        => await useCase.ExecuteAsync(generalTreatmentId);

    /// <summary>
    /// Obtains all specific treatments.
    /// </summary>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [AuthorizeByRole(RoleName.BasicUser, RoleName.Superadmin)]
    [HttpGet]
    public async Task<IEnumerable<GetSpecificTreatmentsResponse>> GetAll(
        GetSpecificTreatmentsUseCase useCase)
        => await useCase.ExecuteAsync();
}
