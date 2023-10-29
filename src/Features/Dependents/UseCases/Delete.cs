namespace DentallApp.Features.Dependents.UseCases;

public class DeleteDependentUseCase
{
    private readonly DbContext _context;

    public DeleteDependentUseCase(DbContext context)
    {
        _context = context;
    }

    public async Task<Result> ExecuteAsync(int dependentId, int userId)
    {
        var dependent = await _context.Set<Dependent>()
            .Where(dependent => dependent.Id == dependentId)
            .Select(dependent => new { dependent.UserId })
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (dependent is null)
            return Result.NotFound();

        if (dependent.UserId != userId)
            return Result.Forbidden(ResourceFromAnotherUserMessage);

        await _context.SoftDeleteAsync<Dependent>(dependentId);
        return Result.DeletedResource();
    }
}
