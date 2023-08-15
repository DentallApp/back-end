namespace DentallApp.Features.Security.Employees.UseCases;

public class GetEmployeesResponse
{
    public class Role
    {
        public int Id { get; init; }
        public string Name { get; init; }
    }

    public class Specialty
    {
        public int Id { get; init; }
        public string Name { get; init; }
    }

    public int EmployeeId { get; init; }
    public int OfficeId { get; init; }
    public string OfficeName { get; init; }
    public string PregradeUniversity { get; init; }
    public string PostgradeUniversity { get; init; }
    public string Document { get; init; }
    public string Names { get; init; }
    public string LastNames { get; init; }
    public string Email { get; init; }
    public string CellPhone { get; init; }
    public DateTime? DateBirth { get; init; }
    public int? GenderId { get; init; }
    public string GenderName { get; init; }
    public IEnumerable<Role> Roles { get; init; }
    public IEnumerable<Specialty> Specialties { get; init; }
    public string Status { get; init; }
    public bool IsDeleted { get; init; }
}

public class GetEmployeesUseCase
{
    private readonly AppDbContext _context;

    public GetEmployeesUseCase(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<GetEmployeesResponse>> Execute(ClaimsPrincipal currentEmployee)
    {
        var queryable = _context.Set<Employee>().AsQueryable();

        if (currentEmployee.IsAdmin())
        {
            int officeId = currentEmployee.GetOfficeId();
            int currentEmployeeId = currentEmployee.GetEmployeeId();
            queryable = queryable.Where(employee => employee.OfficeId == officeId && employee.Id != currentEmployeeId);
        }

        var employees = await queryable
            .Select(employee => new GetEmployeesResponse
            {
                EmployeeId          = employee.Id,
                OfficeId            = employee.OfficeId,
                OfficeName          = employee.Office.Name,
                PostgradeUniversity = employee.PostgradeUniversity,
                PregradeUniversity  = employee.PregradeUniversity,
                Document            = employee.Person.Document,
                Names               = employee.Person.Names,
                LastNames           = employee.Person.LastNames,
                Email               = employee.Person.Email,
                CellPhone           = employee.Person.CellPhone,
                DateBirth           = employee.Person.DateBirth,
                GenderName          = employee.Person.Gender == null ? default : employee.Person.Gender.Name,
                GenderId            = employee.Person.Gender == null ? default : employee.Person.Gender.Id,
                Status              = employee.GetStatusName(),
                IsDeleted           = employee.IsDeleted,

                Roles = employee.User.UserRoles
                    .Select(role => new GetEmployeesResponse.Role
                    {
                        Id   = role.Role.Id,
                        Name = role.Role.Name
                    }),

                Specialties = employee.EmployeeSpecialties
                    .Select(employeeSpecialty => new GetEmployeesResponse.Specialty
                    {
                        Id   = employeeSpecialty.GeneralTreatment.Id,
                        Name = employeeSpecialty.GeneralTreatment.Name
                    })
            })
            .IgnoreQueryFilters()
            .AsNoTracking()
            .ToListAsync();

        return employees;
    }
}
