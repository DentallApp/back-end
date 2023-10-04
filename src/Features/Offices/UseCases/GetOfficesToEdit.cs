namespace DentallApp.Features.Offices.UseCases;

public class GetOfficesToEditResponse
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Address { get; init; }
    public string ContactNumber { get; init; }
    public bool IsDeleted { get; init; }
    public bool IsCheckboxTicked { get; init; }
}

public class GetOfficesToEditUseCase
{
    private readonly DbContext _context;

    public GetOfficesToEditUseCase(DbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<GetOfficesToEditResponse>> ExecuteAsync()
    {
        var offices = await _context.Set<Office>()
            .Select(office => new GetOfficesToEditResponse
            {
                Id               = office.Id,
                Name             = office.Name,
                Address          = office.Address,
                ContactNumber    = office.ContactNumber,
                IsDeleted        = office.IsDeleted,
                IsCheckboxTicked = office.IsCheckboxTicked
            })
            .IgnoreQueryFilters()
            .AsNoTracking()
            .ToListAsync();

        return offices;
    }
}
