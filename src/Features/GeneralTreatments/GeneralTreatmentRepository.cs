namespace DentallApp.Features.GeneralTreatments;

public class GeneralTreatmentRepository : Repository<GeneralTreatment>, IGeneralTreatmentRepository
{
    private readonly AppDbContext _context;

    public GeneralTreatmentRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<GeneralTreatmentGetDto>> GetTreatments()
        => await _context.Set<GeneralTreatment>()
                         .Select(treatment => treatment.MapToGeneralTreatmentGetDto())
                         .ToListAsync();
}
