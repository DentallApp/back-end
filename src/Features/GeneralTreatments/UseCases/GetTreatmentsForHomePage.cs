namespace DentallApp.Features.GeneralTreatments.UseCases;

public class GetGeneralTreatmentsForHomePageResponse
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public string ImageUrl { get; init; }
}

public class GetGeneralTreatmentsForHomePageUseCase
{
    private readonly AppDbContext _context;

    public GetGeneralTreatmentsForHomePageUseCase(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<GetGeneralTreatmentsForHomePageResponse>> ExecuteAsync()
    {
        var generalTreatments = await _context.Set<GeneralTreatment>()
            .Select(treatment => new GetGeneralTreatmentsForHomePageResponse
            {
                Id          = treatment.Id,
                Name        = treatment.Name,
                Description = treatment.Description,
                ImageUrl    = treatment.ImageUrl
            })
            .AsNoTracking()
            .ToListAsync();

        return generalTreatments;
    }
}
