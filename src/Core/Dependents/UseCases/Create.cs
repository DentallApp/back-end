namespace DentallApp.Core.Dependents.UseCases;

public class CreateDependentRequest
{
    public string Document { get; init; }
    public string Names { get; init; }
    public string LastNames { get; init; }
    public string CellPhone { get; init; }
    public DateTime? DateBirth { get; init; }
    public int GenderId { get; init; }
    public string Email { get; init; }
    public int KinshipId { get; init; }

    public Dependent MapToDependent(int userId)
    {
        var person = new Person
        {
            Document  = Document,
            Names     = Names,
            LastNames = LastNames,
            CellPhone = CellPhone,
            Email     = Email,
            DateBirth = DateBirth,
            GenderId  = GenderId
        };
        var dependent = new Dependent
        {
            KinshipId = KinshipId,
            UserId    = userId,
            Person    = person
        };
        return dependent;
    }
}

public class CreateDependentValidator : AbstractValidator<CreateDependentRequest>
{
    public CreateDependentValidator()
    {
        RuleFor(request => request.Document).NotEmpty();
        RuleFor(request => request.Names).NotEmpty();
        RuleFor(request => request.LastNames).NotEmpty();
        RuleFor(request => request.CellPhone).NotEmpty();
        RuleFor(request => request.DateBirth).NotEmpty();
        RuleFor(request => request.Email).EmailAddress();
        RuleFor(request => request.GenderId).GreaterThan(0);
        RuleFor(request => request.KinshipId).GreaterThan(0);
    }
}

public class CreateDependentUseCase(DbContext context, CreateDependentValidator validator)
{
    public async Task<Result<CreatedId>> ExecuteAsync(int userId, CreateDependentRequest request)
    {
        var result = validator.Validate(request);
        if (result.IsFailed())
            return result.Invalid();

        var dependent = request.MapToDependent(userId);
        context.Add(dependent);
        await context.SaveChangesAsync();
        return Result.CreatedResource(dependent.Id);
    }
}
