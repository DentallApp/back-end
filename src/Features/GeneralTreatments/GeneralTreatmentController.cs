using DentallApp.Features.GeneralTreatments.UseCases;

namespace DentallApp.Features.GeneralTreatments;

[Route("general-treatment")]
[ApiController]
public class GeneralTreatmentController
{
    [AuthorizeByRole(RolesName.Superadmin)]
    [HttpPost]
    public async Task<Result<CreatedId>> Create(
        [FromForm]CreateGeneralTreatmentRequest request,
        CreateGeneralTreatmentUseCase useCase)
        => await useCase.ExecuteAsync(request);

    [AuthorizeByRole(RolesName.Superadmin)]
    [HttpPut("{id}")]
    public async Task<Result> Update(
        int id, 
        [FromForm]UpdateGeneralTreatmentRequest request,
        UpdateGeneralTreatmentUseCase useCase)
        => await useCase.ExecuteAsync(id, request);

    [AuthorizeByRole(RolesName.Superadmin)]
    [HttpDelete("{id}")]
    public async Task<Result> Delete(
        int id,
        DeleteGeneralTreatmentUseCase useCase)
        => await useCase.ExecuteAsync(id);

    [HttpGet("{id}")]
    public async Task<Result<GetGeneralTreatmentByIdResponse>> GetById(
        int id,
        GetGeneralTreatmentByIdUseCase useCase)
        => await useCase.ExecuteAsync(id);

    [HttpGet("name")]
    public async Task<IEnumerable<GetGeneralTreatmentNamesResponse>> GetNames(
        GetGeneralTreatmentNamesUseCase useCase)
        => await useCase.ExecuteAsync();

    [HttpGet("edit")]
    public async Task<IEnumerable<GetGeneralTreatmentsToEditResponse>> GetTreatmentsToEdit(
        GetGeneralTreatmentsToEditUseCase useCase)
        => await useCase.ExecuteAsync();

    [HttpGet]
    public async Task<IEnumerable<GetGeneralTreatmentsForHomePageResponse>> GetTreatmentsForHomePage(
        GetGeneralTreatmentsForHomePageUseCase useCase)
        => await useCase.ExecuteAsync();
}
