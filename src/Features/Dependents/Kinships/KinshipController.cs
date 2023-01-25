namespace DentallApp.Features.Dependents.Kinships;

[Route("kinship")]
[ApiController]
public class KinshipController : ControllerBase
{
    private readonly DbContext _context;

    public KinshipController(DbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IEnumerable<KinshipGetDto>> Get()
        => await _context.Set<Kinship>()
                         .Select(kinship => kinship.MapToKinshipGetDto())
                         .ToListAsync();
}
