namespace DentallApp.Features.Kinships;

public class KinshipRepository : Repository<Kinship>, IKinshipRepository
{
    public KinshipRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<KinshipGetDto>> GetKinshipsAsync()
        => await Context.Set<Kinship>()
                        .Select(kinship => kinship.MapToKinshipGetDto())
                        .ToListAsync();
}
