namespace DentallApp.Core.Persons.UseCases;

public class CreatePersonRequest
{
    public string Document { get; init; }
    public string Names { get; init; }
    public string LastNames { get; init; }
    public string CellPhone { get; init; }
    public DateTime? DateBirth { get; init; }
    public int GenderId { get; init; }
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

public class CreatePersonValidator : AbstractValidator<CreatePersonRequest>
{
    public CreatePersonValidator()
    {
        RuleFor(request => request.Document).NotEmpty();
        RuleFor(request => request.Names).NotEmpty();
        RuleFor(request => request.LastNames).NotEmpty();
        RuleFor(request => request.CellPhone).NotEmpty();
        RuleFor(request => request.GenderId).GreaterThan(0);
        RuleFor(request => request.Email).EmailAddress();
    }
}

public class CreatePersonUseCase(DbContext context, CreatePersonValidator validator)
{
    public async Task<Result> ExecuteAsync(CreatePersonRequest request)
    {
        var result = validator.Validate(request);
        if (result.IsFailed())
            return result.Invalid();

        var person = request.MapToPerson();
        context.Add(person);
        await context.SaveChangesAsync();
        return Result.CreatedResource();
    }
}
