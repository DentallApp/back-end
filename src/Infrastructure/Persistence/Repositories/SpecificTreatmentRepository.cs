namespace DentallApp.Infrastructure.Persistence.Repositories;

public class SpecificTreatmentRepository : ISpecificTreatmentRepository
{
    private readonly AppDbContext _context;

    public SpecificTreatmentRepository(AppDbContext context)
    { 
        _context = context;
    }

    public async Task<PayRange> GetRangeToPay(int generalTreatmentId)
    {
        var treatment = await _context.Set<SpecificTreatment>()
            .Where(specificTreatment => specificTreatment.GeneralTreatmentId == generalTreatmentId)
            .GroupBy(specificTreatment => specificTreatment.GeneralTreatmentId)
            .Select(group => new
            {
                PriceMin = group.Min(specificTreatment => specificTreatment.Price),
                PriceMax = group.Max(specificTreatment => specificTreatment.Price)
            })
            .AsNoTracking()
            .FirstOrDefaultAsync();

        return treatment is null ? 
            default : 
            PayRange.Create(treatment.PriceMin, treatment.PriceMax);
    }
}
