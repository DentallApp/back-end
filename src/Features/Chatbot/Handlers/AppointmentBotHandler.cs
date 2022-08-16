namespace DentallApp.Features.Chatbot.Handlers;

public class AppointmentBotHandler<T> : ActivityHandler where T : Dialog
{
    private readonly Dialog _dialog;
    private readonly BotState _conversationState;
    private readonly ILogger _logger;

    public AppointmentBotHandler(T dialog, 
                                 ConversationState conversationState,
                                 ILogger<AppointmentBotHandler<T>> logger)
    {
        _dialog = dialog;
        _conversationState = conversationState;
        _logger = logger;
    }

    protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
    {
        foreach (var member in membersAdded)
        {
            if (member.Id != turnContext.Activity.Recipient.Id)
            {
                await turnContext.SendActivityAsync(
                    MessageFactory.Text("Bienvenido, te saluda Bot, " +
                    "tu asistente para agendamiento de citas \n\nEscribe algo para empezar"),
                    cancellationToken);
            }
        }
    }

    protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
    {
        await _dialog.RunAsync(
            turnContext,
            _conversationState.CreateProperty<DialogState>(nameof(DialogState)),
            cancellationToken
        );
    }

    public override async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default)
    {
        await base.OnTurnAsync(turnContext, cancellationToken);
        await _conversationState.SaveChangesAsync(turnContext, false, cancellationToken);
    }
}