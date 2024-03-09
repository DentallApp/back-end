namespace DentallApp.Core.Security.Users.UseCases;

public class UpdateCurrentUserRequest
{
    public string Names { get; init; }
    public string LastNames { get; init; }
    public string CellPhone { get; init; }
    public DateTime? DateBirth { get; init; }
    public int GenderId { get; init; }

    public void MapToPerson(Person person)
    {
        person.Names     = Names;
        person.LastNames = LastNames;
        person.CellPhone = CellPhone;
        person.DateBirth = DateBirth;
        person.GenderId  = GenderId;
    }
}

public class UpdateCurrentUserValidator : AbstractValidator<UpdateCurrentUserRequest>
{
    public UpdateCurrentUserValidator()
    {
        RuleFor(request => request.Names).NotEmpty();
        RuleFor(request => request.LastNames).NotEmpty();
        RuleFor(request => request.CellPhone).NotEmpty();
        RuleFor(request => request.DateBirth).NotEmpty();
        RuleFor(request => request.GenderId).GreaterThan(0);
    }
}

/// <summary>
/// Current User is the User who is current logged in. 
/// The current user can edit his own information.
/// </summary>
public class UpdateCurrentUserUseCase(
    DbContext context, 
    ICurrentUser currentUser,
    UpdateCurrentUserValidator validator)
{
    public async Task<Result> ExecuteAsync(UpdateCurrentUserRequest request)
    {
        var result = validator.Validate(request);
        if (result.IsFailed())
            return result.Invalid();

        var person = await context.Set<Person>()
            .Where(person => person.Id == currentUser.PersonId)
            .FirstOrDefaultAsync();

        if (person is null)
            return Result.NotFound(Messages.UsernameNotFound);

        if (person.Id != currentUser.PersonId)
            return Result.Forbidden(Messages.CannotUpdateAnotherUserResource);

        request.MapToPerson(person);
        await context.SaveChangesAsync();
        return Result.UpdatedResource();
    }
}
