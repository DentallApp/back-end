using DentallApp.Features.Genders.UseCases;

namespace DentallApp.Features.Genders;

[Route("gender")]
[ApiController]
public class GenderController : ControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<GetGendersResponse>> GetAll(
        [FromServices]GetGendersUseCase useCase)
    {
        return await useCase.Execute();
    }
}
