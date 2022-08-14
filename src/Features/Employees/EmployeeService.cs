namespace DentallApp.Features.Employees;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeService(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public Task<IEnumerable<EmployeeGetDto>> GetEmployeesAsync(ClaimsPrincipal currentEmployee)
        =>  currentEmployee.IsAdmin() 
               ? _employeeRepository.GetFullEmployeesProfileByOfficeIdAsync(currentEmployee.GetEmployeeId(), currentEmployee.GetOfficeId())
               : _employeeRepository.GetFullEmployeesProfileAsync(currentEmployee.GetEmployeeId());

    public async Task<Response> RemoveEmployeeAsync(int id, ClaimsPrincipal currentEmployee)
    {
        var employee = await _employeeRepository.GetEmployeeByIdAsync(id);
        if (employee is null)
            return new Response(EmployeeNotFoundMessage);

        if (currentEmployee.IsAdmin() && currentEmployee.GetOfficeId() != employee.OfficeId)
            return new Response(OfficeNotAssignedMessage);

        if (employee.IsSuperAdmin())
            return new Response(CannotRemoveSuperadminMessage);

        employee.IsDeleted = true;
        employee.UpdatedAt = DateTime.UtcNow;
        await _employeeRepository.SaveAsync();

        return new Response
        {
            Success = true,
            Message = DeleteResourceMessage
        };
    }

    public async Task<Response> EditProfileByCurrentEmployeeAsync(int id, EmployeeUpdateDto employeeUpdateDto)
    {
        var employee = await _employeeRepository.GetDataByIdForCurrentEmployee(id);
        if (employee is null)
            return new Response(EmployeeNotFoundMessage);

        employeeUpdateDto.MapToEmployee(employee);
        await _employeeRepository.SaveAsync();

        return new Response
        {
            Success = true,
            Message = UpdateResourceMessage
        };
    }
}
