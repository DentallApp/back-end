namespace Plugin.ChatBot.DirectLine;

[Route("directline/token")]
[ApiController]
public class DirectLineController(DirectLineService directLineService) : ControllerBase
{
    [AuthorizeByRole(RoleName.BasicUser)]
    [HttpGet]
    public async Task<ActionResult> Get()
    {
        var user = new AuthenticatedUser 
        { 
            UserId   = User.GetUserId(), 
            PersonId = User.GetPersonId() 
        };
        var result = await directLineService.GetTokenAsync(user);
        if (result.IsSuccess)
            return Ok(new { result.Data.Token });

        var objectResult = new ObjectResult(new { result.Message })
        {
            StatusCode = StatusCodes.Status500InternalServerError
        };
        return objectResult;
    }
}
