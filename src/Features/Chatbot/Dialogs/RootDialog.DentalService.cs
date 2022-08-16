namespace DentallApp.Features.Chatbot.Dialogs;

public partial class RootDialog
{
    private async Task<DialogTurnResult> ShowNameOfServices(WaterfallStepContext stepContext, CancellationToken cancellationToken)
    {
        var value = JObject.Parse(stepContext.Context.Activity.Value.ToString());
        stepContext.Values["officeId"] = (string)value["officeId"];
        var choicesTask = _botService.GetDentalServicesAsync();
        var cardJsonTask = TemplateCardLoader.LoadDentalServiceCardAsync();
        var choices = await choicesTask;
        var cardJson = await cardJsonTask;
        return await stepContext.PromptAsync(
            nameof(AdaptiveCardPrompt),
            AdaptiveCardFactory.CreateSingleChoiceCard(cardJson, choices),
            cancellationToken
        );
    }
}
