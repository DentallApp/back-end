namespace DentallApp.Core.Dependents.UseCases;

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

public class UpdateDependentValidator : AbstractValidator<UpdateDependentRequest>
{
    public UpdateDependentValidator()
    {
        RuleFor(request => request.Names).NotEmpty();
        RuleFor(request => request.LastNames).NotEmpty();
        RuleFor(request => request.CellPhone).NotEmpty();
        RuleFor(request => request.DateBirth).NotEmpty();
        RuleFor(request => request.GenderId).GreaterThan(0);
        RuleFor(request => request.KinshipId).GreaterThan(0);
        RuleFor(request => request.Email).EmailAddress();
    }
}

public class UpdateDependentUseCase(
    DbContext context, 
    ICurrentUser currentUser,
    UpdateDependentValidator validator)
{
    public async Task<Result> ExecuteAsync(int id, UpdateDependentRequest request)
    {
        var result = validator.Validate(request);
        if (result.IsFailed())
            return result.Invalid();

        var dependent = await context.Set<Dependent>()
            .Include(dependent => dependent.Person)
            .Where(dependent => dependent.Id == id)
            .FirstOrDefaultAsync();

        if (dependent is null)
            return Result.NotFound();

        if (dependent.UserId != currentUser.UserId)
            return Result.Forbidden(Messages.ResourceFromAnotherUser);

        request.MapToDependent(dependent);
        await context.SaveChangesAsync();
        return Result.UpdatedResource();
    }
}
