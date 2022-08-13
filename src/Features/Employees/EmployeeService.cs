namespace DentallApp.Features.Employees;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeService(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public Task<IEnumerable<EmployeeGetDto>> GetEmployees(ClaimsPrincipal currentEmployee)
        =>  currentEmployee.IsAdmin() 
               ? _employeeRepository.GetFullEmployeesProfileByOfficeId(currentEmployee.GetEmployeeId(), currentEmployee.GetOfficeId())
               : _employeeRepository.GetFullEmployeesProfile(currentEmployee.GetEmployeeId());

    public async Task<Response> RemoveEmployeeAsync(int id, ClaimsPrincipal currentEmployee)
    {
        var employee = await _employeeRepository.GetEmployeeById(id);
        if (employee is null)
            return new Response(EmployeeNotFoundMessage);

        if (currentEmployee.IsAdmin() && currentEmployee.GetOfficeId() != employee.OfficeId)
            return new Response(OfficeNotAssignedMessage);

        if (employee.IsSuperAdmin())
            return new Response(CannotRemoveSuperadminMessage);

        employee.IsDeleted = true;
        await _employeeRepository.SaveAsync();

        return new Response
        {
            Success = true,
            Message = DeleteResourceMessage
        };
    }
}
