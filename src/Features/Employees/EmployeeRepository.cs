namespace DentallApp.Features.Employees;

public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(AppDbContext context) : base(context) { }

    public async Task<Employee> GetEmployeeById(int id)
        => await Context.Set<Employee>()
                        .Include(employee => employee.User.UserRoles)
                        .Where(employee => employee.Id == id)
                        .FirstOrDefaultAsync();

    public async Task<Employee> GetEmployeeByUserId(int userId)
        => await Context.Set<Employee>()
                        .Include(employee => employee.Office)
                        .Where(employee => employee.UserId == userId)
                        .FirstOrDefaultAsync();

    public async Task<IEnumerable<EmployeeGetDto>> GetFullEmployeesProfile(int currentEmployeeId)
        => await Context.Set<Employee>()
                        .Include(employee => employee.Person)
                           .ThenInclude(person => person.Gender)
                        .Include(employee => employee.Office)
                        .Include(employee => employee.User.UserRoles)
                           .ThenInclude(user => user.Role)
                        .Where(employee => employee.Id != currentEmployeeId)
                        .Select(employee => employee.MapToEmployeeGetDto())
                        .ToListAsync();

    public async Task<IEnumerable<EmployeeGetDto>> GetFullEmployeesProfileByOfficeId(int currentEmployeeId, int officeId)
        => await Context.Set<Employee>()
                        .Include(employee => employee.Person)
                           .ThenInclude(person => person.Gender)
                        .Include(employee => employee.Office)
                        .Include(employee => employee.User.UserRoles)
                           .ThenInclude(user => user.Role)
                        .Where(employee => employee.OfficeId == officeId && employee.Id != currentEmployeeId)
                        .Select(employee => employee.MapToEmployeeGetDto())
                        .ToListAsync();
}
