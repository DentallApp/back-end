namespace DentallApp.Features.Employees;

public static class EmployeeMapper
{
    public static EmployeeClaims MapToEmployeeClaims(this EmployeeLoginDto employeeLoginDto)
        => new()
        {
            UserId      = employeeLoginDto.UserId,
            PersonId    = employeeLoginDto.PersonId,
            EmployeeId  = employeeLoginDto.EmployeeId,
            OfficeId    = employeeLoginDto.OfficeId,
            UserName    = employeeLoginDto.UserName,
            FullName    = employeeLoginDto.FullName,
            Roles       = employeeLoginDto.Roles
        };

    public static void MapToEmployeeLoginDto(this Employee employee, EmployeeLoginDto employeeLoginDto)
    {
        employeeLoginDto.EmployeeId          = employee.Id;
        employeeLoginDto.OfficeId            = employee.OfficeId;
        employeeLoginDto.OfficeName          = employee.Office.Name;
        employeeLoginDto.PostgradeUniversity = employee.PostgradeUniversity;
        employeeLoginDto.PregradeUniversity  = employee.PregradeUniversity;
    }
}
