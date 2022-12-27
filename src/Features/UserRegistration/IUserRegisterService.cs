namespace DentallApp.Features.UserRegistration;

public interface IUserRegisterService
{
    Task<Response> CreateBasicUserAccountAsync(UserInsertDto userInsertDto);
    Task<Response<DtoBase>> CreateEmployeeAccountAsync(ClaimsPrincipal currentEmployee, EmployeeInsertDto employeeInsertDto);
}
