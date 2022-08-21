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
                        .FirstOrDefaultAsync();

    public async Task<IEnumerable<EmployeeGetDto>> GetFullEmployeesProfileAsync(int currentEmployeeId)
        => await Context.Set<Employee>()
                        .Include(employee => employee.Person)
                           .ThenInclude(person => person.Gender)
                        .Include(employee => employee.Office)
                        .Include(employee => employee.User.UserRoles)
                           .ThenInclude(user => user.Role)
                        .Where(employee => employee.Id != currentEmployeeId)
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

    public async Task<Employee> GetDataByIdForCurrentEmployee(int id)
        => await Context.Set<Employee>()
                        .Include(employee => employee.Person)
                        .Where(employee => employee.Id == id)
                        .FirstOrDefaultAsync();

    public async Task<Employee> GetDataByIdForAdmin(int id)
        => await Context.Set<Employee>()
                        .Include(employee => employee.Person)
                        .Include(employee => employee.User.UserRoles)
                        .Where(employee => employee.Id == id)
                        .IgnoreQueryFilters()
                        .FirstOrDefaultAsync();
}
