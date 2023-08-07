namespace DentallApp.Features.Security.Users.UseCases;

public class UpdateCurrentUserRequest
{
    public string Names { get; init; }
    public string LastNames { get; init; }
    public string CellPhone { get; init; }
    public DateTime DateBirth { get; init; }
    public int GenderId { get; init; }
}

/// <summary>
/// Current User is the User who is current logged in. The current user can edit his own information.
/// </summary>
public class UpdateCurrentUserUseCase
{
    private readonly AppDbContext _context;

    public UpdateCurrentUserUseCase(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Response> Execute(int currentPersonId, UpdateCurrentUserRequest request)
    {
        var currentUser = await _context.Set<Person>()
            .Where(person => person.Id == currentPersonId)
            .FirstOrDefaultAsync();

        if (currentUser is null)
            return new Response(UsernameNotFoundMessage);

        request.MapToUser(currentUser);
        await _context.SaveChangesAsync();

        return new Response
        {
            Success = true,
            Message = UpdateResourceMessage
        };
    }
}

public static class UpdateCurrentUserMapper
{
    public static void MapToUser(this UpdateCurrentUserRequest request, Person currentUser)
    {
        currentUser.Names     = request.Names;
        currentUser.LastNames = request.LastNames;
        currentUser.CellPhone = request.CellPhone;
        currentUser.DateBirth = request.DateBirth;
        currentUser.GenderId  = request.GenderId;
    }
}
