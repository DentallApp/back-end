using DentallApp.Features.PublicHolidays.UseCases;

namespace DentallApp.Features.PublicHolidays;

[AuthorizeByRole(RoleName.Superadmin)]
[Route("public-holiday")]
[ApiController]
public class PublicHolidayController
{
    [HttpPost]
    public async Task<Result<CreatedId>> Create(
        [FromBody]CreatePublicHolidayRequest request,
        CreatePublicHolidayUseCase useCase)
        => await useCase.ExecuteAsync(request);

    [HttpDelete("{id}")]
    public async Task<Result> Delete(
        int id,
        DeletePublicHolidayUseCase useCase)
        => await useCase.ExecuteAsync(id);

    [HttpPut("{id}")]
    public async Task<Result> Update(
        int id, 
        [FromBody]UpdatePublicHolidayRequest request,
        UpdatePublicHolidayUseCase useCase)
        => await useCase.ExecuteAsync(id, request);

    [HttpGet]
    public async Task<IEnumerable<GetPublicHolidaysResponse>> GetAll(
        GetPublicHolidaysUseCase useCase)
        => await useCase.ExecuteAsync();
}
