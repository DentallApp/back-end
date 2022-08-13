﻿namespace DentallApp.Features.Employees;

public interface IEmployeeService
{
    Task<Response> RemoveEmployeeAsync(int id, ClaimsPrincipal currentEmployee);
    Task<IEnumerable<EmployeeGetDto>> GetEmployeesAsync(ClaimsPrincipal currentEmployee);
}
