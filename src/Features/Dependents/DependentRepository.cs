namespace DentallApp.Features.Dependents;

public class DependentRepository : Repository<Dependent>, IDependentRepository
{
    private readonly AppDbContext _context;

    public DependentRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Dependent> GetDependentByIdAsync(int id)
        => await _context.Set<Dependent>()
                         .Include(dependent => dependent.Person)
                         .Where(dependent => dependent.Id == id)
                         .FirstOrDefaultAsync();

    public async Task<IEnumerable<DependentGetDto>> GetDependentsByUserIdAsync(int userId)
        => await _context.Set<Dependent>()
                         .Include(dependent => dependent.Person)
                            .ThenInclude(person => person.Gender)
                         .Include(dependent => dependent.Kinship)
                         .Where(dependent => dependent.UserId == userId)
                         .Select(dependent => dependent.MapToDependentGetDto())
                         .ToListAsync();
}
