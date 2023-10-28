using DentallApp.Features.SpecificTreatments.UseCases;

namespace DentallApp.Features.SpecificTreatments;

[Route("specific-treatment")]
[ApiController]
public class SpecificTreatmentController : ControllerBase
{
    [AuthorizeByRole(RolesName.Superadmin)]
    [HttpPost]
    public async Task<Result<CreatedId>> Create(
        [FromBody]CreateSpecificTreatmentRequest request,
        [FromServices]CreateSpecificTreatmentUseCase useCase)
    {
        return await useCase.ExecuteAsync(request);
    }

    [AuthorizeByRole(RolesName.Superadmin)]
    [HttpPut("{id}")]
    public async Task<Result> Update(
        int id, 
        [FromBody]UpdateSpecificTreatmentRequest request,
        [FromServices]UpdateSpecificTreatmentUseCase useCase)
    {
        return await useCase.ExecuteAsync(id, request);
    }

    [AuthorizeByRole(RolesName.Superadmin)]
    [HttpDelete("{id}")]
    public async Task<Result> Delete(
        int id,
        [FromServices]DeleteSpecificTreatmentUseCase useCase)
    {
        return await useCase.ExecuteAsync(id);
    }

    [AuthorizeByRole(RolesName.BasicUser, RolesName.Superadmin)]
    [HttpGet("{generalTreatmentId}")]
    public async Task<IEnumerable<GetTreatmentsByGeneralTreatmentIdResponse>> GetByGeneralTreatmentId(
        int generalTreatmentId,
        [FromServices]GetTreatmentsByGeneralTreatmentIdUseCase useCase)
    {
        return await useCase.ExecuteAsync(generalTreatmentId);
    }

    [AuthorizeByRole(RolesName.BasicUser, RolesName.Superadmin)]
    [HttpGet]
    public async Task<IEnumerable<GetSpecificTreatmentsResponse>> GetAll(
        [FromServices]GetSpecificTreatmentsUseCase useCase)
    {
        return await useCase.ExecuteAsync();
    }
}
