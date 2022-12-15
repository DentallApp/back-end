namespace DentallApp.Features.GeneralTreatments;

public class GeneralTreatmentRepository : Repository<GeneralTreatment>, IGeneralTreatmentRepository
{
    public GeneralTreatmentRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<GeneralTreatmentGetDto>> GetTreatmentsAsync()
        => await Context.Set<GeneralTreatment>()
                        .Select(treatment => treatment.MapToGeneralTreatmentGetDto())
                        .ToListAsync();

    public async Task<IEnumerable<GeneralTreatmentShowDto>> GetTreatmentsWithoutImageUrlAsync()
        => await Context.Set<GeneralTreatment>()
                        .Select(treatment => treatment.MapToGeneralTreatmentShowDto())
                        .ToListAsync();

    public async Task<IEnumerable<GeneralTreatmentGetNameDto>> GetTreatmentsWithNameAsync()
        => await Context.Set<GeneralTreatment>()
                        .Select(treatment => treatment.MapToGeneralTreatmentGetNameDto())
                        .ToListAsync();

    public async Task<GeneralTreatmentGetDurationDto> GetTreatmentWithDurationAsync(int treatmentId)
        => await Context.Set<GeneralTreatment>()
                        .Where(treatment => treatment.Id == treatmentId)
                        .Select(treatment => new GeneralTreatmentGetDurationDto { Duration = treatment.Duration })
                        .IgnoreQueryFilters()
                        .FirstOrDefaultAsync();
}
