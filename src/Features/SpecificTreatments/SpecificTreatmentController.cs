using DentallApp.Features.SpecificTreatments.UseCases;

namespace DentallApp.Features.SpecificTreatments;

[Route("specific-treatment")]
[ApiController]
public class SpecificTreatmentController
{
    [AuthorizeByRole(RolesName.Superadmin)]
    [HttpPost]
    public async Task<Result<CreatedId>> Create(
        [FromBody]CreateSpecificTreatmentRequest request,
        CreateSpecificTreatmentUseCase useCase)
        => await useCase.ExecuteAsync(request);

    [AuthorizeByRole(RolesName.Superadmin)]
    [HttpPut("{id}")]
    public async Task<Result> Update(
        int id, 
        [FromBody]UpdateSpecificTreatmentRequest request,
        UpdateSpecificTreatmentUseCase useCase)
        => await useCase.ExecuteAsync(id, request);

    [AuthorizeByRole(RolesName.Superadmin)]
    [HttpDelete("{id}")]
    public async Task<Result> Delete(
        int id,
        DeleteSpecificTreatmentUseCase useCase)
        => await useCase.ExecuteAsync(id);

    [AuthorizeByRole(RolesName.BasicUser, RolesName.Superadmin)]
    [HttpGet("{generalTreatmentId}")]
    public async Task<IEnumerable<GetTreatmentsByGeneralTreatmentIdResponse>> GetByGeneralTreatmentId(
        int generalTreatmentId,
        GetTreatmentsByGeneralTreatmentIdUseCase useCase)
        => await useCase.ExecuteAsync(generalTreatmentId);

    [AuthorizeByRole(RolesName.BasicUser, RolesName.Superadmin)]
    [HttpGet]
    public async Task<IEnumerable<GetSpecificTreatmentsResponse>> GetAll(
        GetSpecificTreatmentsUseCase useCase)
        => await useCase.ExecuteAsync();
}
