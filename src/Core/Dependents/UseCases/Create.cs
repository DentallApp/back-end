namespace DentallApp.Core.Dependents.UseCases;

public class CreateDependentRequest
{
    public string Document { get; init; }
    public string Names { get; init; }
    public string LastNames { get; init; }
    public string CellPhone { get; init; }
    public DateTime? DateBirth { get; init; }
    public int? GenderId { get; init; }
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

public class CreateDependentUseCase(DbContext context)
{
    public async Task<Result<CreatedId>> ExecuteAsync(int userId, CreateDependentRequest request)
    {
        var dependent = request.MapToDependent(userId);
        context.Add(dependent);
        await context.SaveChangesAsync();
        return Result.CreatedResource(dependent.Id);
    }
}
