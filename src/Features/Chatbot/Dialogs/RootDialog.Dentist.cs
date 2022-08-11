namespace DentallApp.Features.Chatbot.Dialogs;

public partial class RootDialog
{
    private async Task<DialogTurnResult> ShowNameOfDentists(WaterfallStepContext stepContext, CancellationToken cancellationToken)
    {
        int officeId = int.Parse(stepContext.Values["officeId"].ToString());
        var choicesTask = _botService.GetDentistsByOfficeIdAsync(officeId);
        var cardJsonTask = TemplateCardLoader.LoadDentistCardAsync();
        var choices = await choicesTask;
        var cardJson = await cardJsonTask;
        return await stepContext.PromptAsync(
            nameof(AdaptiveCardPrompt),
            AdaptiveCardFactory.CreateSingleChoiceCard(cardJson, choices),
            cancellationToken
        );
    }
}
