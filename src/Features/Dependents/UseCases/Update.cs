namespace DentallApp.Features.Dependents.UseCases;

public class UpdateDependentRequest
{
    public string Names { get; init; }
    public string LastNames { get; init; }
    public string CellPhone { get; init; }
    public DateTime? DateBirth { get; init; }
    public int GenderId { get; init; }
    public int KinshipId { get; init; }
    public string Email { get; init; }

    public void MapToDependent(Dependent dependent)
    {
        dependent.Person.Names     = Names;
        dependent.Person.LastNames = LastNames;
        dependent.Person.CellPhone = CellPhone;
        dependent.Person.DateBirth = DateBirth;
        dependent.Person.GenderId  = GenderId;
        dependent.Person.Email     = Email;
        dependent.KinshipId        = KinshipId;
    }
}

public class UpdateDependentUseCase
{
    private readonly DbContext _context;

    public UpdateDependentUseCase(DbContext context)
    {
        _context = context;
    }

    public async Task<Result> ExecuteAsync(int dependentId, int userId, UpdateDependentRequest request)
    {
        var dependent = await _context.Set<Dependent>()
            .Include(dependent => dependent.Person)
            .Where(dependent => dependent.Id == dependentId)
            .FirstOrDefaultAsync();

        if (dependent is null)
            return Result.NotFound();

        if (dependent.UserId != userId)
            return Result.Forbidden(ResourceFromAnotherUserMessage);

        request.MapToDependent(dependent);
        await _context.SaveChangesAsync();
        return Result.UpdatedResource();
    }
}
