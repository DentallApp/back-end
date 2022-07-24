namespace DentallApp.Features.Genders;

public class GenderRepository : Repository<Gender>, IGenderRepository
{
    private readonly AppDbContext _context;

    public GenderRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<GenderGetDto>> GetGenders()
        => await _context.Set<Gender>()
                         .Select(gender => gender.MapToGenderGetDto())
                         .ToListAsync();
}
