﻿namespace DentallApp.Core.Genders.UseCases;

public class GetGendersResponse
{
    public int Id { get; init; }
    public string Name { get; init; }
}

public class GetGendersUseCase(DbContext context)
{
    public async Task<IEnumerable<GetGendersResponse>> ExecuteAsync()
    {
        var genders = await context.Set<Gender>()
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
