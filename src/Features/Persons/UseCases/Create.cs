namespace DentallApp.Features.Persons.UseCases;

public class CreatePersonRequest
{
    public string Document { get; init; }
    public string Names { get; init; }
    public string LastNames { get; init; }
    public string CellPhone { get; init; }
    public DateTime? DateBirth { get; init; }
    public int? GenderId { get; init; }
    public string Email { get; init; }

    public Person MapToPerson()
    {
        return new()
        {
            Document  = Document,
            Names     = Names,
            LastNames = LastNames,
            DateBirth = DateBirth,
            GenderId  = GenderId,
            CellPhone = CellPhone,
            Email     = Email
        };
    }
}

public class CreatePersonUseCase
{
    private readonly AppDbContext _context;

    public CreatePersonUseCase(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Response> Execute(CreatePersonRequest request)
    {
        var person = request.MapToPerson();
        _context.Add(person);
        await _context.SaveChangesAsync();

        return new Response
        {
            Success = true,
            Message = CreateResourceMessage
        };
    }
}
