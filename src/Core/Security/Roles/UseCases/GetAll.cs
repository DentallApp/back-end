namespace DentallApp.Core.Security.Roles.UseCases;

public class GetRolesResponse
{
    public int Id { get; init; }
    public string Name { get; init; }
}

public class GetRolesUseCase(DbContext context)
{
    public async Task<IEnumerable<GetRolesResponse>> ExecuteAsync(bool isSuperadmin)
    {
        var roles = await context.Set<Role>()
            .Select(role => new GetRolesResponse
            {
                Id   = role.Id,
                Name = role.Name
            })
            .AsNoTracking()
            .ToListAsync();

        if (isSuperadmin)
        {
            var response = roles.Where(role =>
                role.Id == (int)Role.Predefined.Secretary ||
                role.Id == (int)Role.Predefined.Dentist ||
                role.Id == (int)Role.Predefined.Admin
            );
            return response;
        }

        return roles.Where(role => role.Id == (int)Role.Predefined.Secretary || role.Id == (int)Role.Predefined.Dentist);
    }
}
