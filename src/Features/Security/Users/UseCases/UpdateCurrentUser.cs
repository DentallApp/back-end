namespace DentallApp.Features.Security.Users.UseCases;

public class UpdateCurrentUserRequest
{
    public string Names { get; init; }
    public string LastNames { get; init; }
    public string CellPhone { get; init; }
    public DateTime DateBirth { get; init; }
    public int GenderId { get; init; }
}

public static class UpdateCurrentUserMapper
{
    public static void MapToPerson(this UpdateCurrentUserRequest request, Person person)
    {
        person.Names     = request.Names;
        person.LastNames = request.LastNames;
        person.CellPhone = request.CellPhone;
        person.DateBirth = request.DateBirth;
        person.GenderId  = request.GenderId;
    }
}

/// <summary>
/// Current User is the User who is current logged in. 
/// The current user can edit his own information.
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
        var person = await _context.Set<Person>()
            .Where(person => person.Id == currentPersonId)
            .FirstOrDefaultAsync();

        if (person is null)
            return new Response(UsernameNotFoundMessage);

        if (person.Id != currentPersonId)
            return new Response(CannotUpdateAnotherUserResource);

        request.MapToPerson(person);
        await _context.SaveChangesAsync();

        return new Response
        {
            Success = true,
            Message = UpdateResourceMessage
        };
    }
}
