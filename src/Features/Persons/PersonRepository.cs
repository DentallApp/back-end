namespace DentallApp.Features.Persons;

public class PersonRepository : Repository<Person>, IPersonRepository
{

    public PersonRepository(AppDbContext context) : base(context)
    {

    }
}
