namespace DentallApp.Features.Employees;

public interface IEmployeeRepository : IRepository<Employee>
{
    Task<Employee> GetEmployeeByUserId(int userId);
    Task<Employee> GetEmployeeById(int id);
    Task<IEnumerable<EmployeeGetDto>> GetFullEmployeesProfile(int currentEmployeeId);
    Task<IEnumerable<EmployeeGetDto>> GetFullEmployeesProfileByOfficeId(int currentEmployeeId, int officeId);
}
