namespace DentallApp.Features.Employees;

public interface IEmployeeRepository : ISoftDeleteRepository<Employee>
{
    Task<Employee> GetEmployeeByUserIdAsync(int userId);
    Task<Employee> GetEmployeeByIdAsync(int id);
    Task<IEnumerable<EmployeeGetDto>> GetFullEmployeesProfileAsync();
    Task<IEnumerable<EmployeeGetDto>> GetFullEmployeesProfileByOfficeIdAsync(int currentEmployeeId, int officeId);
    Task<Employee> GetDataByIdForCurrentEmployeeAsync(int id);
    Task<Employee> GetDataByIdForAdminAsync(int id);

    /// <summary>
    /// Obtiene todos los odontólogos activos e inactivos de un consultorio.
    /// </summary>
    /// <param name="officeId">El ID del consultorio.</param>
    Task<IEnumerable<EmployeeGetByDentistDto>> GetAllDentistsByOfficeIdAsync(int officeId);

    /// <summary>
    /// Obtiene los odontólogos activos de un consultorio.
    /// </summary>
    /// <param name="officeId">El ID del consultorio.</param>
    Task<IEnumerable<EmployeeGetByDentistDto>> GetDentistsByOfficeIdAsync(int officeId);
}
