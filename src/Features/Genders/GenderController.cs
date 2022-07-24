namespace DentallApp.Features.Genders;

[Route("gender")]
[ApiController]
public class GenderController : ControllerBase
{
    private readonly IGenderRepository _repository;

    public GenderController(IGenderRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IEnumerable<GenderGetDto>> Get()
        => await _repository.GetGenders();
}
