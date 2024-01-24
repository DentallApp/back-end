using DentallApp.Features.Kinships.UseCases;

namespace DentallApp.Features.Kinships;

[Route("kinship")]
[ApiController]
public class KinshipController
{
    [HttpGet]
    public async Task<IEnumerable<GetKinshipsResponse>> GetAll(
        GetKinshipsUseCase useCase)
        => await useCase.ExecuteAsync();
}
