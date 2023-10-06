namespace DentallApp.Infrastructure.Persistence.Repositories;

public class TreatmentRepository : ITreatmentRepository
{
    private readonly DbContext _context;

    public TreatmentRepository(DbContext context)
    {
        _context = context;
    }

    public async Task<int?> GetDurationAsync(int generalTreatmentId)
    {
        var treatment = await _context.Set<GeneralTreatment>()
            .Where(treatment => treatment.Id == generalTreatmentId)
            .Select(treatment => new 
            { 
                treatment.Duration 
            })
            .IgnoreQueryFilters()
            .AsNoTracking()
            .FirstOrDefaultAsync();

        return treatment?.Duration;
    }

    public async Task<PayRange> GetRangeToPayAsync(int generalTreatmentId)
    {
        var treatment = await _context.Set<SpecificTreatment>()
            .Where(specificTreatment => specificTreatment.GeneralTreatmentId == generalTreatmentId)
            .GroupBy(specificTreatment => specificTreatment.GeneralTreatmentId)
            .Select(group => new PayRange
            {
                PriceMin = group.Min(specificTreatment => specificTreatment.Price),
                PriceMax = group.Max(specificTreatment => specificTreatment.Price)
            })
            .AsNoTracking()
            .FirstOrDefaultAsync();

        return treatment;
    }
}
