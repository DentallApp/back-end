namespace DentallApp.Features.Offices;

public class OfficeRepository : Repository<Office>, IOfficeRepository
{
    public OfficeRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<OfficeGetDto>> GetOfficesAsync()
        => await Context.Set<Office>()
                        .Select(office => office.MapToOfficeGetDto())
                        .ToListAsync();
}
