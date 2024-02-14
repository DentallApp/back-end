using DentallApp.Core.Genders.UseCases;

namespace DentallApp.Core.Genders;

[Route("gender")]
[ApiController]
public class GenderController
{
    [HttpGet]
    public async Task<IEnumerable<GetGendersResponse>> GetAll(
        GetGendersUseCase useCase)
        => await useCase.ExecuteAsync();
}
