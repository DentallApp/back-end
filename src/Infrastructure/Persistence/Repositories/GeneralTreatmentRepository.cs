namespace DentallApp.Infrastructure.Persistence.Repositories;

public class GeneralTreatmentRepository : IGeneralTreatmentRepository
{
    private readonly AppDbContext _context;

    public GeneralTreatmentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<int?> GetDuration(int treatmentId)
    {
        var treatment = await _context.Set<GeneralTreatment>()
            .Where(treatment => treatment.Id == treatmentId)
            .Select(treatment => new 
            { 
                treatment.Duration 
            })
            .IgnoreQueryFilters()
            .AsNoTracking()
            .FirstOrDefaultAsync();

        return treatment?.Duration;
    }
}
