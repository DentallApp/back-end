using DentallApp.Features.SpecificTreatments.UseCases;

namespace DentallApp.Features.SpecificTreatments;

[Route("specific-treatment")]
[ApiController]
public class SpecificTreatmentController : ControllerBase
{
    [AuthorizeByRole(RolesName.Superadmin)]
    [HttpPost]
    public async Task<ActionResult<Response<InsertedIdDto>>> Create(
        [FromBody]CreateSpecificTreatmentRequest request,
        [FromServices]CreateSpecificTreatmentUseCase useCase)
    {
        var response = await useCase.ExecuteAsync(request);
        return response.Success ? CreatedAtAction(nameof(Create), response) : BadRequest(response);
    }

    [AuthorizeByRole(RolesName.Superadmin)]
    [HttpPut("{id}")]
    public async Task<ActionResult<Response>> Update(
        int id, 
        [FromBody]UpdateSpecificTreatmentRequest request,
        [FromServices]UpdateSpecificTreatmentUseCase useCase)
    {
        var response = await useCase.ExecuteAsync(id, request);
        return response.Success ? Ok(response) : NotFound(response);
    }

    [AuthorizeByRole(RolesName.Superadmin)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<Response>> Delete(
        int id,
        [FromServices]DeleteSpecificTreatmentUseCase useCase)
    {
        var response = await useCase.ExecuteAsync(id);
        return response.Success ? Ok(response) : NotFound(response);
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
