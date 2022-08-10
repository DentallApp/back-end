namespace DentallApp.Features.Chatbot.Dialogs;

public partial class RootDialog : ComponentDialog
{
    private readonly IRepositoriesFactory _repositories;

    public RootDialog(IRepositoriesFactory repositories) : base(nameof(RootDialog))
    {
        _repositories = repositories;

        var waterfallSteps = new WaterfallStep[]
        {
                ShowNameOfPatients,
                ShowNameOfOffices,
                ShowNameOfServices,
                ShowNameOfDentists,
                ShowAppointmentData
        };

        AddDialog(new WaterfallDialog(nameof(WaterfallDialog), waterfallSteps));
        AddDialog(new AdaptiveCardPrompt(nameof(AdaptiveCardPrompt), DialogValidator.ValidateChoiceSet));
    }

    private async Task<DialogTurnResult> ShowNameOfPatients(WaterfallStepContext stepContext, CancellationToken cancellationToken)
    {
        var userProfile = stepContext.CreateUserProfile();
        var choicesTask  = _repositories.CreateBotQueryRepository().GetPatientsAsync(userProfile);
        var cardJsonTask = TemplateCardLoader.LoadPatientCardAsync();
        var choices  = await choicesTask;
        var cardJson = await cardJsonTask;
        return await stepContext.PromptAsync(
            nameof(AdaptiveCardPrompt), 
            AdaptiveCardFactory.CreateSingleChoiceCard(cardJson, choices),
            cancellationToken
        );
    }

    private async Task<DialogTurnResult> ShowNameOfOffices(WaterfallStepContext stepContext, CancellationToken cancellationToken)
    {
        var choicesTask = _repositories.CreateBotQueryRepository().GetOfficesAsync();
        var cardJsonTask = TemplateCardLoader.LoadOfficeCardAsync();
        var choices = await choicesTask;
        var cardJson = await cardJsonTask;
        return await stepContext.PromptAsync(
            nameof(AdaptiveCardPrompt),
            AdaptiveCardFactory.CreateSingleChoiceCard(cardJson, choices),
            cancellationToken
        );
    }

    private async Task<DialogTurnResult> ShowNameOfServices(WaterfallStepContext stepContext, CancellationToken cancellationToken)
    {
        var value = JObject.Parse(stepContext.Context.Activity.Value.ToString());
        stepContext.Values["officeId"] = (string)value["officeId"];
        var choicesTask = _repositories.CreateBotQueryRepository().GetDentalServicesAsync();
        var cardJsonTask = TemplateCardLoader.LoadDentalServiceCardAsync();
        var choices = await choicesTask;
        var cardJson = await cardJsonTask;
        return await stepContext.PromptAsync(
            nameof(AdaptiveCardPrompt),
            AdaptiveCardFactory.CreateSingleChoiceCard(cardJson, choices),
            cancellationToken
        );
    }

    private async Task<DialogTurnResult> ShowNameOfDentists(WaterfallStepContext stepContext, CancellationToken cancellationToken)
    {
        int officeId = int.Parse(stepContext.Values["officeId"].ToString());
        var choicesTask = _repositories.CreateBotQueryRepository().GetDentistsByOfficeIdAsync(officeId);
        var cardJsonTask = TemplateCardLoader.LoadDentistCardAsync();
        var choices = await choicesTask;
        var cardJson = await cardJsonTask;
        return await stepContext.PromptAsync(
            nameof(AdaptiveCardPrompt),
            AdaptiveCardFactory.CreateSingleChoiceCard(cardJson, choices),
            cancellationToken
        );
    }

    private async Task<DialogTurnResult> ShowAppointmentData(WaterfallStepContext stepContext, CancellationToken cancellationToken)
    {
        await stepContext.Context.SendActivityAsync("Bot en construcción... Lo siento...", cancellationToken: cancellationToken);
        return await stepContext.EndDialogAsync(cancellationToken: cancellationToken);
    }
}
