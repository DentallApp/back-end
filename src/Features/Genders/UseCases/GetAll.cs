﻿namespace DentallApp.Features.Genders.UseCases;

public class GetGendersResponse
{
    public int Id { get; init; }
    public string Name { get; init; }
}

public class GetGendersUseCase
{
    private readonly DbContext _context;

    public GetGendersUseCase(DbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<GetGendersResponse>> ExecuteAsync()
    {
        var genders = await _context.Set<Gender>()
            .Select(gender => new GetGendersResponse
            {
                Id   = gender.Id,
                Name = gender.Name
            })
            .AsNoTracking()
            .ToListAsync();

        return genders;
    }
}
