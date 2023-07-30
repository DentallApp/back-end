namespace DentallApp.Features.Dependents;

public class UpdateDependentRequest
{
    public string Names { get; init; }
    public string LastNames { get; init; }
    public string CellPhone { get; init; }
    public DateTime? DateBirth { get; init; }
    public int GenderId { get; init; }
    public int KinshipId { get; init; }
    public string Email { get; init; }
}

public class UpdateDependentHandler
{
    private readonly AppDbContext _context;

    public UpdateDependentHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Response> HandleAsync(int dependentId, int userId, UpdateDependentRequest request)
    {
        var dependent = await _context.Set<Dependent>()
                        .Include(dependent => dependent.Person)
                        .Where(dependent => dependent.Id == dependentId)
                        .FirstOrDefaultAsync();

        if (dependent is null)
            return new Response(ResourceNotFoundMessage);

        if (dependent.UserId != userId)
            return new Response(ResourceFromAnotherUserMessage);

        dependent.Person.Names     = request.Names;
        dependent.Person.LastNames = request.LastNames;
        dependent.Person.CellPhone = request.CellPhone;
        dependent.Person.DateBirth = request.DateBirth;
        dependent.Person.GenderId  = request.GenderId;
        dependent.Person.Email     = request.Email;
        dependent.KinshipId        = request.KinshipId;
        await _context.SaveChangesAsync();

        return new Response
        {
            Success = true,
            Message = UpdateResourceMessage
        };
    }
}
