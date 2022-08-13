namespace DentallApp.Features.UserRegistration;

public interface IUserRegisterService
{
    Task<Response> CreateBasicUserAccountAsync(UserInsertDto userInsertDto);
    Task<Response> CreateEmployeeAccountAsync(ClaimsPrincipal claims, EmployeeInsertDto employeeInsertDto);
}
