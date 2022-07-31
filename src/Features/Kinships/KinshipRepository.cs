namespace DentallApp.Features.Kinships;

public class KinshipRepository : Repository<Kinship>, IKinshipRepository
{
    private readonly AppDbContext _context;

    public KinshipRepository(AppDbContext context) : base(context)
    {
        _context = context;        
    }

    public async Task<IEnumerable<KinshipGetDto>> GetKinshipsAsync()
        => await _context.Set<Kinship>()
                         .Select(kinship => kinship.MapToKinshipGetDto())
                         .ToListAsync();
}
