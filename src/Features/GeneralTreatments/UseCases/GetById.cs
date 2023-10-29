namespace DentallApp.Features.GeneralTreatments.UseCases;

public class GetGeneralTreatmentByIdResponse
{
    public string Name { get; init; }
    public string Description { get; init; }
    public string ImageUrl { get; init; }
}

public class GetGeneralTreatmentByIdUseCase
{
    private readonly DbContext _context;

    public GetGeneralTreatmentByIdUseCase(DbContext context)
    {
        _context = context;
    }

    public async Task<Result<GetGeneralTreatmentByIdResponse>> ExecuteAsync(int id)
    {
        var generalTreatment = await _context.Set<GeneralTreatment>()
            .Where(treatment => treatment.Id == id)
            .Select(treatment => new GetGeneralTreatmentByIdResponse
            {
                Name        = treatment.Name,
                Description = treatment.Description,
                ImageUrl    = treatment.ImageUrl
            })
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (generalTreatment is null)
            return Result.NotFound();

        return Result.ObtainedResource(generalTreatment);
    }
}
