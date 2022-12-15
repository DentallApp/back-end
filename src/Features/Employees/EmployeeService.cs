namespace DentallApp.Features.Employees;

public class EmployeeService : IEmployeeService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;

    public EmployeeService(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher)
    {
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
    }

    public Task<IEnumerable<EmployeeGetDto>> GetEmployeesAsync(ClaimsPrincipal currentEmployee)
        =>  currentEmployee.IsAdmin() 
               ? _unitOfWork.EmployeeRepository.GetFullEmployeesProfileByOfficeIdAsync(currentEmployee.GetEmployeeId(), currentEmployee.GetOfficeId())
               : _unitOfWork.EmployeeRepository.GetFullEmployeesProfileAsync();

    public async Task<Response> RemoveEmployeeAsync(int id, ClaimsPrincipal currentEmployee)
    {
        var employee = await _unitOfWork.EmployeeRepository.GetEmployeeByIdAsync(id);
        if (employee is null)
            return new Response(EmployeeNotFoundMessage);

        if (currentEmployee.IsAdmin() && currentEmployee.IsNotInOffice(employee.OfficeId))
            return new Response(OfficeNotAssignedMessage);

        if (employee.IsSuperAdmin())
            return new Response(CannotRemoveSuperadminMessage);

        _unitOfWork.EmployeeRepository.SoftDelete(employee);
        await _unitOfWork.SaveChangesAsync();

        return new Response
        {
            Success = true,
            Message = DeleteResourceMessage
        };
    }

    public async Task<Response> EditProfileByCurrentEmployeeAsync(int id, EmployeeUpdateDto employeeUpdateDto)
    {
        var employee = await _unitOfWork.EmployeeRepository.GetDataByIdForCurrentEmployeeAsync(id);
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
        var employeeToEdit = await _unitOfWork.EmployeeRepository.GetDataByIdForAdminAsync(employeeId);
        if (employeeToEdit is null)
            return new Response(EmployeeNotFoundMessage);

        // Un admin no puede editar el perfil de un Superadmin.
        // Aunque el Superadmin sí pueda editar su propio perfil.
        if (!currentEmployee.IsSuperAdmin() && employeeToEdit.IsSuperAdmin())
            return new Response(CannotEditSuperadminMessage);

        if (currentEmployee.IsAdmin() && currentEmployee.IsNotInOffice(employeeToEdit.OfficeId))
            return new Response(OfficeNotAssignedMessage);

        if (currentEmployee.HasNotPermissions(employeeUpdateDto.Roles, employeeToEdit.Id))
            return new Response(PermitsNotGrantedMessage);

        if (employeeUpdateDto.Password is not null)
            employeeToEdit.User.Password = _passwordHasher.HashPassword(employeeUpdateDto.Password);
        
        employeeUpdateDto.MapToEmployee(employeeToEdit);

        var userRoles = employeeToEdit.User.UserRoles.OrderBy(userRole => userRole.RoleId);
        var rolesId   = employeeUpdateDto.Roles
                                         .RemoveDuplicates()
                                         .OrderBy(roleId => roleId);
        _unitOfWork.UserRoleRepository.UpdateUserRoles(employeeToEdit.UserId, userRoles, rolesId);
        await _unitOfWork.SaveChangesAsync();

        return new Response
        {
            Success = true,
            Message = UpdateResourceMessage
        };
    }
}
