namespace DentallApp.Features.SpecificTreatments;

public class SpecificTreatmentRepository : ISpecificTreatmentRepository
{
    private readonly AppDbContext _context;

    public SpecificTreatmentRepository(AppDbContext context)
    { 
        _context = context;
    }

    public async Task<SpecificTreatmentRangeToPayDto> GetTreatmentWithRangeToPayAsync(int generalTreatmentId)
    {
        var specificTreatments = await _context.Set<SpecificTreatment>()
            .Where(specificTreatment => specificTreatment.GeneralTreatmentId == generalTreatmentId)
            .GroupBy(specificTreatment => specificTreatment.GeneralTreatmentId)
            .Select(group => new SpecificTreatmentRangeToPayDto
            {
                PriceMin = group.Min(specificTreatment => specificTreatment.Price),
                PriceMax = group.Max(specificTreatment => specificTreatment.Price)
            })
            .AsNoTracking()
            .FirstOrDefaultAsync();

        return specificTreatments;
    }
}
