namespace DentallApp.Features.SpecificTreatments.UseCases;

public class GetSpecificTreatmentsResponse
{
    public int SpecificTreatmentId { get; init; }
    public string SpecificTreatmentName { get; init; }
    public int GeneralTreatmentId { get; init; }
    public string GeneralTreatmentName { get; init; }
    public double Price { get; init; }
}

public class GetSpecificTreatmentsUseCase
{
    private readonly AppDbContext _context;

    public GetSpecificTreatmentsUseCase(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<GetSpecificTreatmentsResponse>> Execute()
    {
        var specificTreatments = await _context.Set<SpecificTreatment>()
            .Select(treatment => new GetSpecificTreatmentsResponse
            {
                SpecificTreatmentId   = treatment.Id,
                SpecificTreatmentName = treatment.Name,
                GeneralTreatmentId    = treatment.GeneralTreatmentId,
                GeneralTreatmentName  = treatment.GeneralTreatment.Name,
                Price                 = treatment.Price
            })
            .AsNoTracking()
            .ToListAsync();

        return specificTreatments;
    }
}
