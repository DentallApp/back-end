namespace DentallApp.Features.Dependents;

public class GetDependentsByUserIdResponse
{
    public string Document { get; init; }
    public string Names { get; init; }
    public string LastNames { get; init; }
    public string FullName => Names + " " + LastNames;
    public string CellPhone { get; init; }
    public DateTime? DateBirth { get; init; }
    public int? GenderId { get; init; }
    public int DependentId { get; init; }
    public string Email { get; init; }
    public string GenderName { get; init; }
    public string KinshipName { get; init; }
    public int KinshipId { get; init; }
}

public class GetDependentsByUserIdUseCase
{
    private readonly AppDbContext _context;

    public GetDependentsByUserIdUseCase(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<GetDependentsByUserIdResponse>> Execute(int userId)
    {
        return await _context.Set<Dependent>()
                             .Include(dependent => dependent.Person)
                                .ThenInclude(person => person.Gender)
                             .Include(dependent => dependent.Kinship)
                             .Where(dependent => dependent.UserId == userId)
                             .Select(dependent => new GetDependentsByUserIdResponse
                             {
                                 DependentId = dependent.Id,
                                 Document    = dependent.Person.Document,
                                 Names       = dependent.Person.Names,
                                 LastNames   = dependent.Person.LastNames,
                                 CellPhone   = dependent.Person.CellPhone,
                                 DateBirth   = dependent.Person.DateBirth,
                                 Email       = dependent.Person.Email,
                                 GenderId    = dependent.Person.GenderId,
                                 GenderName  = dependent.Person.Gender.Name,
                                 KinshipId   = dependent.KinshipId,
                                 KinshipName = dependent.Kinship.Name
                             })
                             .ToListAsync();
    }
}
