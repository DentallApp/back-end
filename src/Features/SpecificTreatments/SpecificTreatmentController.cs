using DentallApp.Features.SpecificTreatments.UseCases;

namespace DentallApp.Features.SpecificTreatments;

[Route("specific-treatment")]
[ApiController]
public class SpecificTreatmentController
{
    [AuthorizeByRole(RoleName.Superadmin)]
    [HttpPost]
    public async Task<Result<CreatedId>> Create(
        [FromBody]CreateSpecificTreatmentRequest request,
        CreateSpecificTreatmentUseCase useCase)
        => await useCase.ExecuteAsync(request);

    [AuthorizeByRole(RoleName.Superadmin)]
    [HttpPut("{id}")]
    public async Task<Result> Update(
        int id, 
        [FromBody]UpdateSpecificTreatmentRequest request,
        UpdateSpecificTreatmentUseCase useCase)
        => await useCase.ExecuteAsync(id, request);

    [AuthorizeByRole(RoleName.Superadmin)]
    [HttpDelete("{id}")]
    public async Task<Result> Delete(
        int id,
        DeleteSpecificTreatmentUseCase useCase)
        => await useCase.ExecuteAsync(id);

    [AuthorizeByRole(RoleName.BasicUser, RoleName.Superadmin)]
    [HttpGet("{generalTreatmentId}")]
    public async Task<IEnumerable<GetTreatmentsByGeneralTreatmentIdResponse>> GetByGeneralTreatmentId(
        int generalTreatmentId,
        GetTreatmentsByGeneralTreatmentIdUseCase useCase)
        => await useCase.ExecuteAsync(generalTreatmentId);

    [AuthorizeByRole(RoleName.BasicUser, RoleName.Superadmin)]
    [HttpGet]
    public async Task<IEnumerable<GetSpecificTreatmentsResponse>> GetAll(
        GetSpecificTreatmentsUseCase useCase)
        => await useCase.ExecuteAsync();
}
