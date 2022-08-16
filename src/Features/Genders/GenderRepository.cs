namespace DentallApp.Features.Genders;

public class GenderRepository : Repository<Gender>, IGenderRepository
{
    public GenderRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<GenderGetDto>> GetGendersAsync()
        => await Context.Set<Gender>()
                        .Select(gender => gender.MapToGenderGetDto())
                        .ToListAsync();
}
