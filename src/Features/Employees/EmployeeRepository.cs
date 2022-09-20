namespace DentallApp.Features.Employees;

public class EmployeeRepository : SoftDeleteRepository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(AppDbContext context) : base(context) { }

    public async Task<Employee> GetEmployeeByIdAsync(int id)
        => await Context.Set<Employee>()
                        .Include(employee => employee.User.UserRoles)
                        .Where(employee => employee.Id == id)
                        .FirstOrDefaultAsync();

    public async Task<Employee> GetEmployeeByUserIdAsync(int userId)
        => await Context.Set<Employee>()
                        .Include(employee => employee.Office)
                        .Where(employee => employee.UserId == userId)
                        .IgnoreQueryFilters()
                        .FirstOrDefaultAsync();

    public async Task<IEnumerable<EmployeeGetDto>> GetFullEmployeesProfileAsync()
        => await Context.Set<Employee>()
                        .Include(employee => employee.Person)
                           .ThenInclude(person => person.Gender)
                        .Include(employee => employee.Office)
                        .Include(employee => employee.User.UserRoles)
                           .ThenInclude(user => user.Role)
                        .Select(employee => employee.MapToEmployeeGetDto())
                        .IgnoreQueryFilters()
                        .ToListAsync();

    public async Task<IEnumerable<EmployeeGetDto>> GetFullEmployeesProfileByOfficeIdAsync(int currentEmployeeId, int officeId)
        => await Context.Set<Employee>()
                        .Include(employee => employee.Person)
                           .ThenInclude(person => person.Gender)
                        .Include(employee => employee.Office)
                        .Include(employee => employee.User.UserRoles)
                           .ThenInclude(user => user.Role)
                        .Where(employee => employee.OfficeId == officeId && employee.Id != currentEmployeeId)
                        .Select(employee => employee.MapToEmployeeGetDto())
                        .IgnoreQueryFilters()
                        .ToListAsync();

    public async Task<Employee> GetDataByIdForCurrentEmployeeAsync(int id)
        => await Context.Set<Employee>()
                        .Include(employee => employee.Person)
                        .Where(employee => employee.Id == id)
                        .FirstOrDefaultAsync();

    public async Task<Employee> GetDataByIdForAdminAsync(int id)
        => await Context.Set<Employee>()
                        .Include(employee => employee.Person)
                        .Include(employee => employee.User.UserRoles)
                        .Where(employee => employee.Id == id)
                        .IgnoreQueryFilters()
                        .FirstOrDefaultAsync();

    private IQueryable<EmployeeGetByDentistDto> GetDentistsQueryable(int officeId)
    {
        var query = (from employee in Context.Set<Employee>()
                     join person in Context.Set<Person>() on employee.PersonId equals person.Id
                     join userRole in Context.Set<UserRole>() on employee.UserId equals userRole.UserId
                     where employee.OfficeId == officeId && userRole.RoleId == RolesId.Dentist
                     select employee.MapToEmployeeGetByDentistDto(person));
        return query;
    }

    public async Task<IEnumerable<EmployeeGetByDentistDto>> GetAllDentistsByOfficeIdAsync(int officeId)
        => await GetDentistsQueryable(officeId)
                 .IgnoreQueryFilters()
                 .ToListAsync();

    public async Task<IEnumerable<EmployeeGetByDentistDto>> GetDentistsByOfficeIdAsync(int officeId)
        => await GetDentistsQueryable(officeId).ToListAsync();

    public async Task<IEnumerable<EmployeeGetByDentistDto>> GetAllDentistsAsync()
        => await (from employee in Context.Set<Employee>()
                  join person in Context.Set<Person>() on employee.PersonId equals person.Id
                  join userRole in Context.Set<UserRole>() on employee.UserId equals userRole.UserId
                  where userRole.RoleId == RolesId.Dentist
                  select employee.MapToEmployeeGetByDentistDto(person))
                .IgnoreQueryFilters()
                .ToListAsync();
}
