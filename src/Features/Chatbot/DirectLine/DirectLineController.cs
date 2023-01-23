namespace DentallApp.Features.Chatbot.DirectLine;

[Route("directline/token")]
[ApiController]
public class DirectLineController : ControllerBase
{
	private readonly DirectLineService _directLineService;

	public DirectLineController(DirectLineService directLineService)
    {
        _directLineService = directLineService;
    }

    [AuthorizeByRole(RolesName.BasicUser)]
    [HttpGet]
    public async Task<ActionResult> Get()
    {
        var response = await _directLineService.GetTokenAsync(User.GetUserId());
        return response.Success ? Ok(response.Data.Token) : BadRequest(response.Message);
    }
}
