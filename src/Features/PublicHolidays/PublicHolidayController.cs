using DentallApp.Features.PublicHolidays.UseCases;

namespace DentallApp.Features.PublicHolidays;

[AuthorizeByRole(RolesName.Superadmin)]
[Route("public-holiday")]
[ApiController]
public class PublicHolidayController : ControllerBase
{
    [HttpPost]
    public async Task<Result<CreatedId>> Create(
        [FromBody]CreatePublicHolidayRequest request,
        [FromServices]CreatePublicHolidayUseCase useCase)
    {
        return await useCase.ExecuteAsync(request);
    }

    [HttpDelete("{id}")]
    public async Task<Result> Delete(
        int id,
        [FromServices]DeletePublicHolidayUseCase useCase)
    {
        return await useCase.ExecuteAsync(id);
    }

    [HttpPut("{id}")]
    public async Task<Result> Update(
        int id, 
        [FromBody]UpdatePublicHolidayRequest request,
        [FromServices]UpdatePublicHolidayUseCase useCase)
    {
        return await useCase.ExecuteAsync(id, request);
    }

    [HttpGet]
    public async Task<IEnumerable<GetPublicHolidaysResponse>> GetAll(
        [FromServices]GetPublicHolidaysUseCase useCase)
    {
        return await useCase.ExecuteAsync();
    }
}
