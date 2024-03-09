namespace DentallApp.Core.Dependents.UseCases;

public class DeleteDependentUseCase(DbContext context, ICurrentUser currentUser)
{
    public async Task<Result> ExecuteAsync(int id)
    {
        var dependent = await context.Set<Dependent>()
            .Where(dependent => dependent.Id == id)
            .Select(dependent => new { dependent.UserId })
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (dependent is null)
            return Result.NotFound();

        if (dependent.UserId != currentUser.UserId)
            return Result.Forbidden(Messages.ResourceFromAnotherUser);

        await context.SoftDeleteAsync<Dependent>(id);
        return Result.DeletedResource();
    }
}
