namespace DentallApp.Features.Dependents.Kinships;

[Route("kinship")]
[ApiController]
public class KinshipController : ControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<GetAllKinshipsResponse>> GetAll(
        [FromServices]GetAllKinshipsHandler handler)
    {
        return await handler.HandleAsync();
    }
}
