namespace DentallApp.Features.GeneralTreatments;

public class GeneralTreatmentRepository : IGeneralTreatmentRepository
{
    private readonly AppDbContext _context;

    public GeneralTreatmentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<GeneralTreatmentGetDurationDto> GetTreatmentWithDurationAsync(int treatmentId)
    {
        var durations = await _context.Set<GeneralTreatment>()
            .Where(treatment => treatment.Id == treatmentId)
            .Select(treatment => new GeneralTreatmentGetDurationDto 
            { 
                Duration = treatment.Duration 
            })
            .IgnoreQueryFilters()
            .AsNoTracking()
            .FirstOrDefaultAsync();

        return durations;
    }
}
