namespace DentallApp.Features.Dependents.Kinships;

[Route("kinship")]
[ApiController]
public class KinshipController : ControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<GetAllKinshipsResponse>> GetAll(
        [FromServices]GetAllKinshipsUseCase useCase)
    {
        return await useCase.Execute();
    }
}
