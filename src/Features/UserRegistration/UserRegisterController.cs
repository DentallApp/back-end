namespace DentallApp.Features.UserRegistration;

[Route("register")]
[ApiController]
public class UserRegisterController : ControllerBase
{
    private readonly UserRegisterService _userRegisterService;

    public UserRegisterController(UserRegisterService userRegisterService)
    {
        _userRegisterService = userRegisterService;
    }

    [Route("basic-user")]
    [HttpPost]
    public async Task<ActionResult<Response>> CreateBasicUserAccount([FromBody]UserInsertDto userInsertDto)
    {
        var response = await _userRegisterService.CreateBasicUserAccountAsync(userInsertDto);
        return response.Success ? 
               CreatedAtAction(nameof(CreateBasicUserAccount), response) : 
               BadRequest(response);
    }

    [AuthorizeByRole(RolesName.Admin, RolesName.Superadmin)]
    [Route("employee")]
    [HttpPost]
    public async Task<ActionResult<Response<DtoBase>>> CreateEmployeeAccount([FromBody]EmployeeInsertDto employeeInsertDto)
    {
        var response = await _userRegisterService.CreateEmployeeAccountAsync(User, employeeInsertDto);
        return response.Success ? 
               CreatedAtAction(nameof(CreateEmployeeAccount), response) : 
               BadRequest(response);
    }
}
