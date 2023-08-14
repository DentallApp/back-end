namespace DentallApp.Features.SpecificTreatments.UseCases;

public class GetTreatmentsByGeneralTreatmentIdResponse
{
    public int Id { get; init; }
    public string Name { get; init; }
    public double Price { get; init; }
}

public class GetTreatmentsByGeneralTreatmentIdUseCase
{
    private readonly AppDbContext _context;

    public GetTreatmentsByGeneralTreatmentIdUseCase(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<GetTreatmentsByGeneralTreatmentIdResponse>> Execute(int generalTreatmentId)
    {
        var specificTreatments = await
        (from specificTreatment in _context.Set<SpecificTreatment>()
            join generalTreatment in _context.Set<GeneralTreatment>()
            on specificTreatment.GeneralTreatmentId equals generalTreatment.Id
            where specificTreatment.GeneralTreatmentId == generalTreatmentId
            select new GetTreatmentsByGeneralTreatmentIdResponse
            {
                Id    = specificTreatment.Id,
                Name  = specificTreatment.Name,
                Price = specificTreatment.Price
            }
        ).ToListAsync();

        return specificTreatments;
    }
}
