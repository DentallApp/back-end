﻿namespace DentallApp.Core.Security.Users.UseCases;

public class UpdateCurrentUserRequest
{
    public string Names { get; init; }
    public string LastNames { get; init; }
    public string CellPhone { get; init; }
    public DateTime DateBirth { get; init; }
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

/// <summary>
/// Current User is the User who is current logged in. 
/// The current user can edit his own information.
/// </summary>
public class UpdateCurrentUserUseCase(DbContext context)
{
    public async Task<Result> ExecuteAsync(int currentPersonId, UpdateCurrentUserRequest request)
    {
        var person = await context.Set<Person>()
            .Where(person => person.Id == currentPersonId)
            .FirstOrDefaultAsync();

        if (person is null)
            return Result.NotFound(Messages.UsernameNotFound);

        if (person.Id != currentPersonId)
            return Result.Forbidden(Messages.CannotUpdateAnotherUserResource);

        request.MapToPerson(person);
        await context.SaveChangesAsync();
        return Result.UpdatedResource();
    }
}