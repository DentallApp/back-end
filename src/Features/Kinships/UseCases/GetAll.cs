namespace DentallApp.Features.Kinships.UseCases;

public class GetKinshipsResponse
{
    public int Id { get; init; }
    public string Name { get; init; }
}

public class GetKinshipsUseCase
{
    private readonly AppDbContext _context;

    public GetKinshipsUseCase(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<GetKinshipsResponse>> Execute()
    {
        var kinships = await _context.Set<Kinship>()
            .Select(kinship => new GetKinshipsResponse
            {
                Id = kinship.Id,
                Name = kinship.Name
            })
            .AsNoTracking()
            .ToListAsync();

        return kinships;
    }
}
