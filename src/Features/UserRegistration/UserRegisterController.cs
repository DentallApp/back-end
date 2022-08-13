namespace DentallApp.Features.UserRegistration;

[Route("register")]
[ApiController]
public class UserRegisterController : ControllerBase
{
    private readonly IUserRegisterService _userRegisterService;

    public UserRegisterController(IUserRegisterService userRegisterService)
    {
        _userRegisterService = userRegisterService;
    }

    [Route("basic-user")]
    [HttpPost]
    public async Task<ActionResult<Response>> CreateBasicUserAccount([FromBody]UserInsertDto userInsertDto)
    {
        var response = await _userRegisterService.CreateBasicUserAccountAsync(userInsertDto);
        if (response.Success)
            return CreatedAtAction(nameof(CreateBasicUserAccount), response);

        return BadRequest(response);
    }

    [AuthorizeByRole(RolesName.Admin, RolesName.Superadmin)]
    [Route("employee")]
    [HttpPost]
    public async Task<ActionResult<Response>> CreateEmployeeAccount([FromBody]EmployeeInsertDto employeeInsertDto)
    {
        var response = await _userRegisterService.CreateEmployeeAccountAsync(User, employeeInsertDto);
        if (response.Success)
            return CreatedAtAction(nameof(CreateEmployeeAccount), response);

        return BadRequest(response);
    }
}
