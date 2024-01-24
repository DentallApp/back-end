namespace DentallApp.Features.Kinships.UseCases;

public class GetKinshipsResponse
{
    public int Id { get; init; }
    public string Name { get; init; }
}

public class GetKinshipsUseCase(DbContext context)
{
    public async Task<IEnumerable<GetKinshipsResponse>> ExecuteAsync()
    {
        var kinships = await context.Set<Kinship>()
            .Select(kinship => new GetKinshipsResponse
            {
                Id   = kinship.Id,
                Name = kinship.Name
            })
            .AsNoTracking()
            .ToListAsync();

        return kinships;
    }
}
