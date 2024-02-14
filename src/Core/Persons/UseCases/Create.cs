namespace DentallApp.Core.Persons.UseCases;

public class CreatePersonRequest
{
    public string Document { get; init; }
    public string Names { get; init; }
    public string LastNames { get; init; }
    public string CellPhone { get; init; }
    public DateTime? DateBirth { get; init; }
    public int? GenderId { get; init; }
    public string Email { get; init; }

    public Person MapToPerson() => new()
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

public class CreatePersonUseCase(DbContext context)
{
    public async Task<Result> ExecuteAsync(CreatePersonRequest request)
    {
        var person = request.MapToPerson();
        context.Add(person);
        await context.SaveChangesAsync();
        return Result.CreatedResource();
    }
}
