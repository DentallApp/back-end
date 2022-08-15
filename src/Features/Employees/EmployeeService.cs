namespace DentallApp.Features.Employees;

public class EmployeeService : IEmployeeService
{
    private readonly IUnitOfWork _unitOfWork;

    public EmployeeService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task<IEnumerable<EmployeeGetDto>> GetEmployeesAsync(ClaimsPrincipal currentEmployee)
        =>  currentEmployee.IsAdmin() 
               ? _unitOfWork.EmployeeRepository.GetFullEmployeesProfileByOfficeIdAsync(currentEmployee.GetEmployeeId(), currentEmployee.GetOfficeId())
               : _unitOfWork.EmployeeRepository.GetFullEmployeesProfileAsync(currentEmployee.GetEmployeeId());

    public async Task<Response> RemoveEmployeeAsync(int id, ClaimsPrincipal currentEmployee)
    {
        var employee = await _unitOfWork.EmployeeRepository.GetEmployeeByIdAsync(id);
        if (employee is null)
            return new Response(EmployeeNotFoundMessage);

        if (currentEmployee.IsAdmin() && currentEmployee.IsNotInOffice(employee.OfficeId))
            return new Response(OfficeNotAssignedMessage);

        if (employee.IsSuperAdmin())
            return new Response(CannotRemoveSuperadminMessage);

        employee.IsDeleted = true;
        await _unitOfWork.SaveChangesAsync();

        return new Response
        {
            Success = true,
            Message = DeleteResourceMessage
        };
    }

    public async Task<Response> EditProfileByCurrentEmployeeAsync(int id, EmployeeUpdateDto employeeUpdateDto)
    {
        var employee = await _unitOfWork.EmployeeRepository.GetDataByIdForCurrentEmployee(id);
        if (employee is null)
            return new Response(EmployeeNotFoundMessage);

        employeeUpdateDto.MapToEmployee(employee);
        await _unitOfWork.SaveChangesAsync();

        return new Response
        {
            Success = true,
            Message = UpdateResourceMessage
        };
    }

    public async Task<Response> EditProfileByAdminAsync(int employeeId, ClaimsPrincipal currentEmployee, EmployeeUpdateByAdminDto employeeUpdateDto)
    {
        var employee = await _unitOfWork.EmployeeRepository.GetDataByIdForAdmin(employeeId);
        if (employee is null)
            return new Response(EmployeeNotFoundMessage);

        if (employee.IsSuperAdmin())
            return new Response(CannotEditSuperadminMessage);

        if (currentEmployee.IsAdmin() && currentEmployee.IsNotInOffice(employee.OfficeId))
            return new Response(OfficeNotAssignedMessage);

        if (currentEmployee.HasNotPermissions(employeeUpdateDto.Roles))
            return new Response(PermitsNotGrantedMessage);

        employeeUpdateDto.MapToEmployee(employee);


        var userRoles = employee.User.UserRoles.OrderBy(userRole => userRole.RoleId);
        var rolesId   = employeeUpdateDto.Roles
                                         .RemoveDuplicates()
                                         .OrderBy(roleId => roleId);
        _unitOfWork.UserRoleRepository.UpdateUserRoles(employee.UserId, userRoles, rolesId);
        await _unitOfWork.SaveChangesAsync();

        return new Response
        {
            Success = true,
            Message = UpdateResourceMessage
        };
    }
}
