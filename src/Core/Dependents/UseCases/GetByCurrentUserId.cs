﻿namespace DentallApp.Core.Dependents.UseCases;

public class GetDependentsByCurrentUserIdResponse
{
    public string Document { get; init; }
    public string Names { get; init; }
    public string LastNames { get; init; }
    public string FullName { get; init; }
    public string CellPhone { get; init; }
    public DateTime? DateBirth { get; init; }
    public int? GenderId { get; init; }
    public int DependentId { get; init; }
    public string Email { get; init; }
    public string GenderName { get; init; }
    public string KinshipName { get; init; }
    public int KinshipId { get; init; }
}

public class GetDependentsByCurrentUserIdUseCase(DbContext context, ICurrentUser currentUser)
{
    public async Task<IEnumerable<GetDependentsByCurrentUserIdResponse>> ExecuteAsync()
    {
        var dependents = await context.Set<Dependent>()
            .Where(dependent => dependent.UserId == currentUser.UserId)
            .Select(dependent => new GetDependentsByCurrentUserIdResponse
            {
                DependentId = dependent.Id,
                Document    = dependent.Person.Document,
                Names       = dependent.Person.Names,
                LastNames   = dependent.Person.LastNames,
                FullName    = dependent.Person.FullName,
                CellPhone   = dependent.Person.CellPhone,
                DateBirth   = dependent.Person.DateBirth,
                Email       = dependent.Person.Email,
                GenderId    = dependent.Person.GenderId,
                GenderName  = dependent.Person.Gender.Name,
                KinshipId   = dependent.KinshipId,
                KinshipName = dependent.Kinship.Name
            })
            .AsNoTracking()
            .ToListAsync();

        return dependents;
    }
}
