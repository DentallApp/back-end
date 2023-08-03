namespace DentallApp.Features.Dependents;

public class DeleteDependentUseCase
{
    private readonly AppDbContext _context;

    public DeleteDependentUseCase(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Response> Execute(int dependentId, int userId)
    {
        var dependent = await _context.Set<Dependent>()
            .Where(dependent => dependent.Id == dependentId)
            .Select(dependent => new { dependent.UserId })
            .FirstOrDefaultAsync();

        if (dependent is null)
            return new Response(ResourceNotFoundMessage);

        if (dependent.UserId != userId)
            return new Response(ResourceFromAnotherUserMessage);

        await _context.SoftDeleteAsync<Dependent>(dependentId);
        return new Response
        {
            Success = true,
            Message = DeleteResourceMessage
        };
    }
}
