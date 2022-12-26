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

    public static Employee MapToEmployee(this EmployeeInsertDto employeeInsertDto)
        => new()
        {
            OfficeId            = employeeInsertDto.OfficeId,
            PostgradeUniversity = employeeInsertDto.PostgradeUniversity,
            PregradeUniversity  = employeeInsertDto.PregradeUniversity
        };

    [Decompile]
    public static EmployeeGetDto MapToEmployeeGetDto(this Employee employee)
        => new()
        {
            EmployeeId              = employee.Id,
            OfficeId                = employee.OfficeId,
            OfficeName              = employee.Office.Name,
            PostgradeUniversity     = employee.PostgradeUniversity,
            PregradeUniversity      = employee.PregradeUniversity,
            Document                = employee.Person.Document,
            Names                   = employee.Person.Names,
            LastNames               = employee.Person.LastNames,
            Email                   = employee.Person.Email,
            CellPhone               = employee.Person.CellPhone,
            DateBirth               = employee.Person.DateBirth,
            GenderName              = employee.Person.Gender?.Name,
            GenderId                = employee.Person.Gender?.Id,
            Roles                   = employee.User.UserRoles.Select(role => 
                                                                     new RoleGetDto 
                                                                     { 
                                                                         Id   = role.Role.Id, 
                                                                         Name = role.Role.Name
                                                                     }),
            Specialties             = employee.EmployeeSpecialties.Select(employeeSpecialty =>
                                                                     new GeneralTreatmentGetNameDto
                                                                     {
                                                                         Id   = employeeSpecialty.GeneralTreatment.Id,
                                                                         Name = employeeSpecialty.GeneralTreatment.Name
                                                                     }),
            Status                  = employee.GetStatusName(),
            IsDeleted               = employee.IsDeleted
        };

    public static void MapToEmployee(this EmployeeUpdateDto employeeUpdateDto, Employee employee)
    {
        employee.PregradeUniversity     = employeeUpdateDto.PregradeUniversity;
        employee.PostgradeUniversity    = employeeUpdateDto.PostgradeUniversity;
        employeeUpdateDto.MapToPerson(employee.Person);
    }

    public static void MapToEmployee(this EmployeeUpdateByAdminDto employeeUpdateDto, Employee employee)
    {
        MapToEmployee((EmployeeUpdateDto)employeeUpdateDto, employee);
        employee.OfficeId           = employeeUpdateDto.OfficeId;
        employee.Person.Document    = employeeUpdateDto.Document;
        employee.Person.Email       = employeeUpdateDto.Email;
        employee.User.UserName      = employeeUpdateDto.Email;
        employee.IsDeleted          = employeeUpdateDto.IsDeleted;
        if(employee.IsInactive())
        {
            employee.User.RefreshToken = null;
            employee.User.RefreshTokenExpiry = null;
        }
    }

    [Decompile]
    public static EmployeeGetByDentistDto MapToEmployeeGetByDentistDto(this Employee employee, Person person)
        => new()
        {
            EmployeeId = employee.Id,
            FullName   = person.FullName
        };
}
