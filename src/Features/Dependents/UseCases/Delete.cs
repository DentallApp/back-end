﻿namespace DentallApp.Features.Dependents.UseCases;

public class DeleteDependentUseCase
{
    private readonly DbContext _context;

    public DeleteDependentUseCase(DbContext context)
    {
        _context = context;
    }

    public async Task<Response> ExecuteAsync(int dependentId, int userId)
    {
        var dependent = await _context.Set<Dependent>()
            .Where(dependent => dependent.Id == dependentId)
            .Select(dependent => new { dependent.UserId })
            .AsNoTracking()
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
