namespace DentallApp.Features.Employees;

public interface IEmployeeRepository : IRepository<Employee>
{
    Task<Employee> GetEmployeeByUserIdAsync(int userId);
    Task<Employee> GetEmployeeByIdAsync(int id);
    Task<IEnumerable<EmployeeGetDto>> GetFullEmployeesProfileAsync(int currentEmployeeId);
    Task<IEnumerable<EmployeeGetDto>> GetFullEmployeesProfileByOfficeIdAsync(int currentEmployeeId, int officeId);
    Task<Employee> GetDataByIdForCurrentEmployee(int id);
    Task<Employee> GetDataByIdForAdmin(int id);
}
