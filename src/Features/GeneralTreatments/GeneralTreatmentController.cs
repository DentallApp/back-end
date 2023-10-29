using DentallApp.Features.GeneralTreatments.UseCases;

namespace DentallApp.Features.GeneralTreatments;

[Route("general-treatment")]
[ApiController]
public class GeneralTreatmentController : ControllerBase
{
    [AuthorizeByRole(RolesName.Superadmin)]
    [HttpPost]
    public async Task<Result<CreatedId>> Create(
        [FromForm]CreateGeneralTreatmentRequest request,
        [FromServices]CreateGeneralTreatmentUseCase useCase)
    {
        return await useCase.ExecuteAsync(request);
    }

    [AuthorizeByRole(RolesName.Superadmin)]
    [HttpPut("{id}")]
    public async Task<Result> Update(
        int id, 
        [FromForm]UpdateGeneralTreatmentRequest request,
        [FromServices]UpdateGeneralTreatmentUseCase useCase)
    {
        return await useCase.ExecuteAsync(id, request);
    }

    [AuthorizeByRole(RolesName.Superadmin)]
    [HttpDelete("{id}")]
    public async Task<Result> Delete(
        int id,
        [FromServices]DeleteGeneralTreatmentUseCase useCase)
    {
        return await useCase.ExecuteAsync(id);
    }

    [HttpGet("{id}")]
    public async Task<Result<GetGeneralTreatmentByIdResponse>> GetById(
        int id,
        [FromServices]GetGeneralTreatmentByIdUseCase useCase)
    {
        return await useCase.ExecuteAsync(id);
    }

    [HttpGet("name")]
    public async Task<IEnumerable<GetGeneralTreatmentNamesResponse>> GetNames(
        [FromServices]GetGeneralTreatmentNamesUseCase useCase)
    {
        return await useCase.ExecuteAsync();
    }

    [HttpGet("edit")]
    public async Task<IEnumerable<GetGeneralTreatmentsToEditResponse>> GetTreatmentsToEdit(
        [FromServices]GetGeneralTreatmentsToEditUseCase useCase)
    {
        return await useCase.ExecuteAsync();
    }

    [HttpGet]
    public async Task<IEnumerable<GetGeneralTreatmentsForHomePageResponse>> GetTreatmentsForHomePage(
        [FromServices]GetGeneralTreatmentsForHomePageUseCase useCase)
    {
        return await useCase.ExecuteAsync();
    }
}
