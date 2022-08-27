namespace DentallApp.Features.Appoinments;

[Route("appoinment")]
[ApiController]
public class AppoinmentController : ControllerBase
{
    private readonly IAppoinmentService _appoinmentService;

    public AppoinmentController(IAppoinmentService appoinmentService)
    {
        _appoinmentService = appoinmentService;
    }

    [AuthorizeByRole(RolesName.BasicUser)]
    [HttpGet("basic-user")]
    public async Task<IEnumerable<AppoinmentGetByBasicUserDto>> GetAppoinmentsByUserId()
        => await _appoinmentService.GetAppoinmentsByUserIdAsync(User.GetUserId());
}
