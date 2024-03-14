using DentallApp.Core.GeneralTreatments.UseCases;

namespace DentallApp.Core.GeneralTreatments;

[Route("general-treatment")]
[ApiController]
public class GeneralTreatmentController
{
    /// <summary>
    /// Creates a new general treatment.
    /// </summary>
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType<Result>(StatusCodes.Status400BadRequest)]
    [AuthorizeByRole(RoleName.Superadmin)]
    [HttpPost]
    public async Task<Result<CreatedId>> Create(
        [FromForm]CreateGeneralTreatmentRequest request,
        CreateGeneralTreatmentUseCase useCase)
        => await useCase.ExecuteAsync(request);

    /// <summary>
    /// Updates a general treatment by its ID.
    /// </summary>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<Result>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<Result>(StatusCodes.Status404NotFound)]
    [AuthorizeByRole(RoleName.Superadmin)]
    [HttpPut("{id}")]
    public async Task<Result> Update(
        int id, 
        [FromForm]UpdateGeneralTreatmentRequest request,
        UpdateGeneralTreatmentUseCase useCase)
        => await useCase.ExecuteAsync(id, request);

    /// <summary>
    /// Deletes a general treatment by its ID.
    /// </summary>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<Result>(StatusCodes.Status404NotFound)]
    [AuthorizeByRole(RoleName.Superadmin)]
    [HttpDelete("{id}")]
    public async Task<Result> Delete(
        int id,
        DeleteGeneralTreatmentUseCase useCase)
        => await useCase.ExecuteAsync(id);

    /// <summary>
    /// Gets the general treatment by ID.
    /// </summary>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<Result>(StatusCodes.Status404NotFound)]
    [HttpGet("{id}")]
    public async Task<Result<GetGeneralTreatmentByIdResponse>> GetById(
        int id,
        GetGeneralTreatmentByIdUseCase useCase)
        => await useCase.ExecuteAsync(id);

    /// <summary>
    /// Gets only the names of each general treatment.
    /// </summary>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet("name")]
    public async Task<IEnumerable<GetGeneralTreatmentNamesResponse>> GetNames(
        GetGeneralTreatmentNamesUseCase useCase)
        => await useCase.ExecuteAsync();

    /// <summary>
    /// Gets the general treatments that will be used to edit from a form.
    /// </summary>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet("edit")]
    public async Task<IEnumerable<GetGeneralTreatmentsToEditResponse>> GetTreatmentsToEdit(
        GetGeneralTreatmentsToEditUseCase useCase)
        => await useCase.ExecuteAsync();

    /// <summary>
    /// Gets the general treatments that will be displayed on the home page.
    /// </summary>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet]
    public async Task<IEnumerable<GetGeneralTreatmentsForHomePageResponse>> GetTreatmentsForHomePage(
        GetGeneralTreatmentsForHomePageUseCase useCase)
        => await useCase.ExecuteAsync();
}
