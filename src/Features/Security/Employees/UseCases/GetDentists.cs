namespace DentallApp.Features.Security.Employees.UseCases;

public class GetDentistsRequest
{
    public int OfficeId { get; init; }
    public bool? IsDentistDeleted { get; init; }
}

public class GetDentistsResponse
{
    public int EmployeeId { get; init; }
    public string FullName { get; init; }
}

public class GetDentistsUseCase
{
    private readonly AppDbContext _context;

    public GetDentistsUseCase(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<GetDentistsResponse>> Execute(GetDentistsRequest request)
    {
        var queryable = 
           (from employee in _context.Set<Employee>()
            join person in _context.Set<Person>() on employee.PersonId equals person.Id
            join userRole in _context.Set<UserRole>() on employee.UserId equals userRole.UserId
            where userRole.RoleId == RolesId.Dentist
            select new { employee, person });

        var dentists = await queryable
            .OptionalWhere(request.OfficeId, o => o.employee.OfficeId == request.OfficeId)
            .OptionalWhere(request.IsDentistDeleted, o => o.employee.IsDeleted == request.IsDentistDeleted)
            .Select(o => new GetDentistsResponse
            {
                EmployeeId = o.employee.Id,
                FullName   = o.person.FullName
            })
            .IgnoreQueryFilters()
            .AsNoTracking()
            .ToListAsync();

        return dentists;
    }
}
