namespace DentallApp.Features.PersonalInformation;

public class PersonRepository : Repository<Person>, IPersonRepository
{
    public PersonRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<PersonGetDto>> GetPersonsAsync(string valueToSearch)
        => await Context.Set<Person>()
                        .Where(person =>
                               person.Names.Contains(valueToSearch) ||
                               person.LastNames.Contains(valueToSearch) ||
                               person.Document.Contains(valueToSearch))
                        .Select(person => person.MapToPersonGetDto())
                        .ToListAsync();
}
