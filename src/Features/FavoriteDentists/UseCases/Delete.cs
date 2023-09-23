﻿namespace DentallApp.Features.FavoriteDentists.UseCases;

public class DeleteFavoriteDentistRequest
{
    public int UserId { get; init; }
    public int FavoriteDentistId { get; init; }
}

/// <summary>
/// Represents the use case to delete a favorite dentist of a basic user.
/// </summary>
public class DeleteFavoriteDentistUseCase
{
    private readonly AppDbContext _context;

    public DeleteFavoriteDentistUseCase(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Executes the use case. Deletes by favorite dentist id.
    /// </summary>
    /// <param name="request">Contains the user id and favorite dentist id.</param>
    public async Task<Response> ExecuteAsync(DeleteFavoriteDentistRequest request)
    {
        var favoriteDentist = await _context.Set<FavoriteDentist>()
            .Where(favoriteDentist => favoriteDentist.Id == request.FavoriteDentistId)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (favoriteDentist is null)
            return new Response(ResourceNotFoundMessage);

        if (favoriteDentist.UserId != request.UserId)
            return new Response(ResourceFromAnotherUserMessage);

        _context.Remove(favoriteDentist);
        await _context.SaveChangesAsync();

        return new Response
        {
            Success = true,
            Message = DeleteResourceMessage
        };
    }

    /// <summary>
    /// Executes the use case. Deletes by user id and dentist id.
    /// </summary>
    /// <param name="userId">The basic user id.</param>
    /// <param name="dentistId">The dentist id.</param>
    public async Task<Response> ExecuteAsync(int userId, int dentistId)
    {
        int deletedRows = await _context.Set<FavoriteDentist>()
            .Where(favoriteDentist =>
                favoriteDentist.UserId == userId &&
                favoriteDentist.DentistId == dentistId)
            .ExecuteDeleteAsync();

        if (deletedRows == 0)
            return new Response(ResourceNotFoundMessage);

        return new Response
        {
            Success = true,
            Message = DeleteResourceMessage
        };
    }
}
