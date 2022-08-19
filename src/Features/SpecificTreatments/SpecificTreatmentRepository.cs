namespace DentallApp.Features.SpecificTreatments;

public class SpecificTreatmentRepository : Repository<SpecificTreatment>, ISpecificTreatmentRepository
{
    public SpecificTreatmentRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<SpecificTreatmentShowDto>> GetSpecificTreatmentsAsync()
        => await Context.Set<SpecificTreatment>()
                        .Include(specificTreatment => specificTreatment.GeneralTreatment)
                        .Select(specificTreatment  => specificTreatment.MapToSpecificTreatmentShowDto())
                        .ToListAsync();

    public async Task<IEnumerable<SpecificTreatmentGetDto>> GetSpecificTreatmentsByGeneralTreatmentIdAsync(int generalTreatmentId)
        => await (from specificTreatment in Context.Set<SpecificTreatment>()
                  join generalTreatment in Context.Set<GeneralTreatment>() 
                    on specificTreatment.GeneralTreatmentId equals generalTreatment.Id
                  where specificTreatment.GeneralTreatmentId == generalTreatmentId
                  select specificTreatment.MapToSpecificTreatmentGetDto()
                 ).ToListAsync();
}
