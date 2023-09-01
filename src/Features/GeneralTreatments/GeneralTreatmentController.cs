using DentallApp.Features.GeneralTreatments.UseCases;

namespace DentallApp.Features.GeneralTreatments;

[Route("general-treatment")]
[ApiController]
public class GeneralTreatmentController : ControllerBase
{
    [AuthorizeByRole(RolesName.Superadmin)]
    [HttpPost]
    public async Task<ActionResult<Response<InsertedIdDto>>> Create(
        [FromForm]CreateGeneralTreatmentRequest request,
        [FromServices]CreateGeneralTreatmentUseCase useCase)
    {
        var response = await useCase.Execute(request);
        return response.Success ? CreatedAtAction(nameof(Create), response) : BadRequest(response);
    }

    [AuthorizeByRole(RolesName.Superadmin)]
    [HttpPut("{id}")]
    public async Task<ActionResult<Response>> Update(
        int id, 
        [FromForm]UpdateGeneralTreatmentRequest request,
        [FromServices]UpdateGeneralTreatmentUseCase useCase)
    {
        var response = await useCase.Execute(id, request);
        return response.Success ? Ok(response) : NotFound(response);
    }

    [AuthorizeByRole(RolesName.Superadmin)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<Response>> Delete(
        int id,
        [FromServices]DeleteGeneralTreatmentUseCase useCase)
    {
        var response = await useCase.Execute(id);
        return response.Success ? Ok(response) : NotFound(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Response<GetGeneralTreatmentByIdResponse>>> GetById(
        int id,
        [FromServices]GetGeneralTreatmentByIdUseCase useCase)
    {
        var response = await useCase.Execute(id);
        return response.Success ? Ok(response) : NotFound(response);
    }

    [HttpGet("name")]
    public async Task<IEnumerable<GetGeneralTreatmentNamesResponse>> GetNames(
        [FromServices]GetGeneralTreatmentNamesUseCase useCase)
    {
        return await useCase.Execute();
    }

    [HttpGet("edit")]
    public async Task<IEnumerable<GetGeneralTreatmentsToEditResponse>> GetTreatmentsToEdit(
        [FromServices]GetGeneralTreatmentsToEditUseCase useCase)
    {
        return await useCase.Execute();
    }

    [HttpGet]
    public async Task<IEnumerable<GetGeneralTreatmentsForHomePageResponse>> GetTreatmentsForHomePage(
        [FromServices]GetGeneralTreatmentsForHomePageUseCase useCase)
    {
        return await useCase.Execute();
    }
}
