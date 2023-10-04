namespace DentallApp.Features.Persons.UseCases;

public class GetPersonsResponse
{
    public int PersonId { get; init; }
    public string Document { get; init; }
    public string Names { get; init; }
    public string LastNames { get; init; }
    public string FullName { get; init; }
    public string CellPhone { get; init; }
    public string Email { get; init; }
}

public class GetPersonsUseCase
{
    private readonly DbContext _context;

    public GetPersonsUseCase(DbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<GetPersonsResponse>> ExecuteAsync(string valueToSearch)
    {
        var persons = await _context.Set<Person>()
            .Where(person =>
                    person.Names.Contains(valueToSearch) ||
                    person.LastNames.Contains(valueToSearch) ||
                    person.Document.Contains(valueToSearch))
            .Select(person => new GetPersonsResponse
            {
                PersonId    = person.Id,
                Document    = person.Document,
                Names       = person.Names,
                LastNames   = person.LastNames,
                FullName    = person.FullName,
                CellPhone   = person.CellPhone,
                Email       = person.Email
            })
            .AsNoTracking()
            .ToListAsync();

        return persons;
    }
}
