namespace DentallApp.Features.GeneralTreatments.UseCases;

public class GetGeneralTreatmentsToEditResponse
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public int Duration { get; init; }
}

public class GetGeneralTreatmentsToEditUseCase(DbContext context)
{
    public async Task<IEnumerable<GetGeneralTreatmentsToEditResponse>> ExecuteAsync()
    {
        var generalTreatments = await context.Set<GeneralTreatment>()
            .Select(treatment => new GetGeneralTreatmentsToEditResponse
            {
                Id          = treatment.Id,
                Name        = treatment.Name,
                Description = treatment.Description,
                Duration    = treatment.Duration
            })
            .AsNoTracking()
            .ToListAsync();

        return generalTreatments;
    }
}
