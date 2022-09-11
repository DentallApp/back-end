namespace DentallApp.Features.Employees;

public interface IEmployeeRepository : ISoftDeleteRepository<Employee>
{
    Task<Employee> GetEmployeeByUserIdAsync(int userId);
    Task<Employee> GetEmployeeByIdAsync(int id);
    Task<IEnumerable<EmployeeGetDto>> GetFullEmployeesProfileAsync();
    Task<IEnumerable<EmployeeGetDto>> GetFullEmployeesProfileByOfficeIdAsync(int currentEmployeeId, int officeId);
    Task<Employee> GetDataByIdForCurrentEmployeeAsync(int id);
    Task<Employee> GetDataByIdForAdminAsync(int id);
    Task<IEnumerable<EmployeeGetByDentistDto>> GetAllDentistsByOfficeIdAsync(int officeId);
}
