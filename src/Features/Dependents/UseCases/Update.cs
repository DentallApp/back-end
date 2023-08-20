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
    private readonly AppDbContext _context;

    public UpdateDependentUseCase(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Response> Execute(int dependentId, int userId, UpdateDependentRequest request)
    {
        var dependent = await _context.Set<Dependent>()
            .Include(dependent => dependent.Person)
            .Where(dependent => dependent.Id == dependentId)
            .FirstOrDefaultAsync();

        if (dependent is null)
            return new Response(ResourceNotFoundMessage);

        if (dependent.UserId != userId)
            return new Response(ResourceFromAnotherUserMessage);

        request.MapToDependent(dependent);
        await _context.SaveChangesAsync();

        return new Response
        {
            Success = true,
            Message = UpdateResourceMessage
        };
    }
}
