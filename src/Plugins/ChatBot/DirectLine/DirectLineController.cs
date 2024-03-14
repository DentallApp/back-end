namespace Plugin.ChatBot.DirectLine;

[Route("directline/token")]
[ApiController]
public class DirectLineController(
    DirectLineService directLineService, 
    ICurrentUser currentUser)
{
    /// <summary>
    /// Gets the Direct Line token to be able to communicate with the bot.
    /// </summary>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<Result>(StatusCodes.Status500InternalServerError)]
    [AuthorizeByRole(RoleName.BasicUser)]
    [HttpGet]
    public async Task<Result<GetDirectLineTokenResponse>> Get()
        => await directLineService.GetTokenAsync(new AuthenticatedUser
        {
            UserId   = currentUser.UserId,
            PersonId = currentUser.PersonId
        });
}
