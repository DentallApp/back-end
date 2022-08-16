namespace DentallApp.Features.Chatbot.Dialogs;

public partial class RootDialog
{
    private async Task<DialogTurnResult> ShowNameOfPatients(WaterfallStepContext stepContext, CancellationToken cancellationToken)
    {
        var userProfile = stepContext.CreateUserProfile();
        var choicesTask = _botService.GetPatientsAsync(userProfile);
        var cardJsonTask = TemplateCardLoader.LoadPatientCardAsync();
        var choices = await choicesTask;
        var cardJson = await cardJsonTask;
        return await stepContext.PromptAsync(
            nameof(AdaptiveCardPrompt),
            AdaptiveCardFactory.CreateSingleChoiceCard(cardJson, choices),
            cancellationToken
        );
    }
}
