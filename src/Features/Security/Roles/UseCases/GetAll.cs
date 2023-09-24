namespace DentallApp.Features.Security.Roles.UseCases;

public class GetRolesResponse
{
    public int Id { get; init; }
    public string Name { get; init; }
}

public class GetRolesUseCase
{
    private readonly AppDbContext _context;

    public GetRolesUseCase(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<GetRolesResponse>> ExecuteAsync(bool isSuperadmin)
    {
        var roles = await _context.Set<Role>()
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
                role.Id == RolesId.Secretary ||
                role.Id == RolesId.Dentist ||
                role.Id == RolesId.Admin
            );
            return response;
        }

        return roles.Where(role => role.Id == RolesId.Secretary || role.Id == RolesId.Dentist);
    }
}
