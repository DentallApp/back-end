namespace DentallApp.Features.Employees;

public interface IEmployeeRepository : IRepository<Employee>
{
    Task<Employee> GetEmployeeByUserId(int userId);
}
