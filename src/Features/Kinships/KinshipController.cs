namespace DentallApp.Features.Kinships;

[Route("kinship")]
[ApiController]
public class KinshipController : ControllerBase
{
    private readonly IKinshipRepository _repository;

    public KinshipController(IKinshipRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IEnumerable<KinshipGetDto>> Get()
        => await _repository.GetKinshipsAsync();
}
