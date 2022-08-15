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
}
