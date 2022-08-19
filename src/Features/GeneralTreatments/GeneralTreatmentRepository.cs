namespace DentallApp.Features.GeneralTreatments;

public class GeneralTreatmentRepository : SoftDeleteRepository<GeneralTreatment>, IGeneralTreatmentRepository
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
}
