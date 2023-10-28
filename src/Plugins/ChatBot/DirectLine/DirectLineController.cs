namespace DentallApp.Features.ChatBot.DirectLine;

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
        var result = await _directLineService.GetTokenAsync(user);
        return result.IsSuccess ? 
			   Ok(new { result.Data.Token }) : 
			   UnprocessableEntity(new { result.Message });
    }
}
