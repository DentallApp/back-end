namespace DentallApp.Features.Dependents.Kinships;

[Route("kinship")]
[ApiController]
public class KinshipController : ControllerBase
{
    private readonly AppDbContext _context;

    public KinshipController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IEnumerable<KinshipGetDto>> Get()
        => await _context.Set<Kinship>()
                         .Select(kinship => kinship.MapToKinshipGetDto())
                         .ToListAsync();
}
