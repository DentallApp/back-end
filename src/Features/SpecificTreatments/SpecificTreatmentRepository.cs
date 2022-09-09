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

    public async Task<SpecificTreatmentRangeToPayDto> GetTreatmentWithRangeToPayAsync(int generalTreatmentId)
        => await Context.Set<SpecificTreatment>()
                        .Where(specificTreatment => specificTreatment.GeneralTreatmentId == generalTreatmentId)
                        .GroupBy(specificTreatment => specificTreatment.GeneralTreatmentId)
                        .Select(group => new SpecificTreatmentRangeToPayDto
                         {
                            PriceMin = group.Min(specificTreatment => specificTreatment.Price),
                            PriceMax = group.Max(specificTreatment => specificTreatment.Price)
                         })
                        .FirstOrDefaultAsync();
}
