using DentallApp.Features.Kinships.UseCases;

namespace DentallApp.Features.Kinships;

[Route("kinship")]
[ApiController]
public class KinshipController : ControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<GetKinshipsResponse>> GetAll(
        [FromServices] GetKinshipsUseCase useCase)
    {
        return await useCase.Execute();
    }
}
