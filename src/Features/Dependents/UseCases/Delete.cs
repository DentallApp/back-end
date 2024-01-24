namespace DentallApp.Features.Dependents.UseCases;

public class DeleteDependentUseCase(DbContext context)
{
    public async Task<Result> ExecuteAsync(int dependentId, int userId)
    {
        var dependent = await context.Set<Dependent>()
            .Where(dependent => dependent.Id == dependentId)
            .Select(dependent => new { dependent.UserId })
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (dependent is null)
            return Result.NotFound();

        if (dependent.UserId != userId)
            return Result.Forbidden(ResourceFromAnotherUserMessage);

        await context.SoftDeleteAsync<Dependent>(dependentId);
        return Result.DeletedResource();
    }
}
