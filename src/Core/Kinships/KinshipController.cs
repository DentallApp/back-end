using DentallApp.Core.Kinships.UseCases;

namespace DentallApp.Core.Kinships;

[Route("kinship")]
[ApiController]
public class KinshipController
{
    [HttpGet]
    public async Task<IEnumerable<GetKinshipsResponse>> GetAll(
        GetKinshipsUseCase useCase)
        => await useCase.ExecuteAsync();
}
