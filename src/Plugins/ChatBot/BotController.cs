namespace DentallApp.Features.ChatBot;

// This ASP Controller is created to handle a request. Dependency Injection will provide the Adapter and IBot
// implementation at runtime. Multiple different IBot implementations running at different endpoints can be
// achieved by specifying a more specific type for the bot constructor argument.
[ApiExplorerSettings(IgnoreApi = true)]
[Route("messages")]
[ApiController]
public class BotController(IBotFrameworkHttpAdapter adapter, IBot bot) : ControllerBase
{
    [HttpPost]
    [HttpGet]
    public async Task Post()
    {
        // Delegate the processing of the HTTP POST to the adapter.
        // The adapter will invoke the bot.
        await adapter.ProcessAsync(Request, Response, bot);
    }
}
