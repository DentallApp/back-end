using DentallApp.Features.Genders.UseCases;

namespace DentallApp.Features.Genders;

[Route("gender")]
[ApiController]
public class GenderController
{
    [HttpGet]
    public async Task<IEnumerable<GetGendersResponse>> GetAll(
        GetGendersUseCase useCase)
        => await useCase.ExecuteAsync();
}
