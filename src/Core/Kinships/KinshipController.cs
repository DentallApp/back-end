using DentallApp.Core.Kinships.UseCases;

namespace DentallApp.Core.Kinships;

[Route("kinship")]
[ApiController]
public class KinshipController
{
    /// <summary>
    /// Gets a list of kinship.
    /// </summary>
    [HttpGet]
    public async Task<IEnumerable<GetKinshipsResponse>> GetAll(
        GetKinshipsUseCase useCase)
        => await useCase.ExecuteAsync();
}
