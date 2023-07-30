namespace DentallApp.Features.Dependents.Kinships;

public class GetAllKinshipsResponse
{
    public int Id { get; init; }
    public string Name { get; init; }
}

public class GetAllKinshipsHandler
{
    private readonly AppDbContext _context;

    public GetAllKinshipsHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<GetAllKinshipsResponse>> HandleAsync()
    {
        return await _context.Set<Kinship>()
                             .Select(kinship => new GetAllKinshipsResponse
                             {
                                 Id   = kinship.Id,
                                 Name = kinship.Name
                             })
                             .ToListAsync();
    }
}
