namespace DentallApp.Features.Chatbot.Dialogs;

public partial class RootDialog
{
    private async Task<bool> ValidateChoiceSet(PromptValidatorContext<JObject> promptContext, CancellationToken cancellationToken)
        => await Task.FromResult(promptContext.Context.Activity.Value != null);
}
