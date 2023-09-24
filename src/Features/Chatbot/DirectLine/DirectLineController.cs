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
        var user = new AuthenticatedUser 
        { 
            UserId   = User.GetUserId(), 
            PersonId = User.GetPersonId() 
        };
        var response = await _directLineService.GetTokenAsync(user);
        return response.Success ? 
			   Ok(new { response.Data.Token }) : 
			   BadRequest(new { response.Message });
    }
}
