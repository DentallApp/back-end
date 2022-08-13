namespace DentallApp.Features.Employees;

public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(AppDbContext context) : base(context) { }

    public async Task<Employee> GetEmployeeByUserId(int userId)
        => await Context.Set<Employee>()
                        .Include(employee => employee.Office)
                        .Where(employee => employee.UserId == userId)
                        .FirstOrDefaultAsync();
}
