using DentallApp.Features.PublicHolidays.UseCases;

namespace DentallApp.Features.PublicHolidays;

[AuthorizeByRole(RolesName.Superadmin)]
[Route("public-holiday")]
[ApiController]
public class PublicHolidayController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Response<InsertedIdDto>>> Create(
        [FromBody]CreatePublicHolidayRequest request,
        [FromServices]CreatePublicHolidayUseCase useCase)
    {
        var response = await useCase.Execute(request);
        return response.Success ? CreatedAtAction(nameof(Create), response) : BadRequest(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Response>> Delete(
        int id,
        [FromServices]DeletePublicHolidayUseCase useCase)
    {
        var response = await useCase.Execute(id);
        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Response>> Update(
        int id, 
        [FromBody]UpdatePublicHolidayRequest request,
        [FromServices]UpdatePublicHolidayUseCase useCase)
    {
        var response = await useCase.Execute(id, request);
        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpGet]
    public async Task<IEnumerable<GetPublicHolidaysResponse>> GetAll(
        [FromServices]GetPublicHolidaysUseCase useCase)
    {
        return await useCase.Execute();
    }
}
