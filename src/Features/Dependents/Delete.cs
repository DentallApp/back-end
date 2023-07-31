namespace DentallApp.Features.Dependents;

public class DeleteDependentUseCase
{
    private readonly AppDbContext _context;

    public DeleteDependentUseCase(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Response> HandleAsync(int dependentId, int userId)
    {
        var dependent = await _context.Set<Dependent>()
                        .Include(dependent => dependent.Person)
                        .Where(dependent => dependent.Id == dependentId)
                        .FirstOrDefaultAsync();

        if (dependent is null)
            return new Response(ResourceNotFoundMessage);

        if (dependent.UserId != userId)
            return new Response(ResourceFromAnotherUserMessage);

        _context.SoftDelete(dependent);
        await _context.SaveChangesAsync();

        return new Response
        {
            Success = true,
            Message = DeleteResourceMessage
        };
    }
}
