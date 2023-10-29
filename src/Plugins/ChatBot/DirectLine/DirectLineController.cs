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
        if (result.IsSuccess)
            return Ok(new { result.Data.Token });

        var objectResult = new ObjectResult(new { result.Message })
        {
            StatusCode = StatusCodes.Status500InternalServerError
        };
        return objectResult;
    }
}
