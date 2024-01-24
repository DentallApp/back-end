namespace DentallApp.Features.Offices.UseCases;

public class GetOfficeNamesResponse
{
    public int Id { get; init; }
    public string Name { get; init; }
}

public class GetOfficeNamesUseCase(DbContext context)
{
    public async Task<IEnumerable<GetOfficeNamesResponse>> ExecuteAsync(bool? status)
    {
        var offices = await context.Set<Office>()
            .OptionalWhere(status, office => office.IsActive() == status)
            .Select(office => new GetOfficeNamesResponse
            {
                Id   = office.Id,
                Name = office.Name
            })
            .IgnoreQueryFilters()
            .AsNoTracking()
            .ToListAsync();

        return offices;
    }
}
