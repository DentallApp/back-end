namespace DentallApp.Features.Employees;

public interface IEmployeeService
{
    Task<Response> RemoveEmployeeAsync(int id, ClaimsPrincipal currentEmployee);
    Task<IEnumerable<EmployeeGetDto>> GetEmployeesAsync(ClaimsPrincipal currentEmployee);
    Task<Response> EditProfileByCurrentEmployeeAsync(int id, EmployeeUpdateDto employeeUpdateDto);
    Task<Response> EditProfileByAdminAsync(int employeeId, ClaimsPrincipal currentEmployee, EmployeeUpdateByAdminDto employeeUpdateDto);
    Task<IEnumerable<EmployeeGetByDentistDto>> GetDentistsByOfficeIdAsync(int officeId);
}
