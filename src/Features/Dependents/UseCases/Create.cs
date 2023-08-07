namespace DentallApp.Features.Dependents.UseCases;

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
}

public static class CreateDependentMapper
{
    public static Dependent MapToDependent(this CreateDependentRequest request, int userId)
    {
        var person = new Person
        {
            Document  = request.Document,
            Names     = request.Names,
            LastNames = request.LastNames,
            CellPhone = request.CellPhone,
            Email     = request.Email,
            DateBirth = request.DateBirth,
            GenderId  = request.GenderId
        };
        var dependent = new Dependent
        {
            KinshipId = request.KinshipId,
            UserId    = userId,
            Person    = person
        };
        return dependent;
    }
}

public class CreateDependentUseCase
{
    private readonly AppDbContext _context;

    public CreateDependentUseCase(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Response<InsertedIdDto>> Execute(int userId, CreateDependentRequest request)
    {
        var dependent = request.MapToDependent(userId);
        _context.Add(dependent);
        await _context.SaveChangesAsync();

        return new Response<InsertedIdDto>
        {
            Data    = new InsertedIdDto { Id = dependent.Id },
            Success = true,
            Message = CreateResourceMessage
        };
    }
}
