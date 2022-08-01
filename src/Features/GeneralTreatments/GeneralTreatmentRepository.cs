namespace DentallApp.Features.GeneralTreatments;

public class GeneralTreatmentRepository : Repository<GeneralTreatment>, IGeneralTreatmentRepository
{
    public GeneralTreatmentRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<GeneralTreatmentGetDto>> GetTreatmentsAsync()
        => await Context.Set<GeneralTreatment>()
                        .Select(treatment => treatment.MapToGeneralTreatmentGetDto())
                        .ToListAsync();
}
