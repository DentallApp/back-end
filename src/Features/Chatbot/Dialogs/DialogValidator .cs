namespace DentallApp.Features.Chatbot.Dialogs;

public class DialogValidator
{
    public static async Task<bool> ValidateChoiceSet(PromptValidatorContext<JObject> promptContext, CancellationToken cancellationToken)
        => await Task.FromResult(promptContext.Context.Activity.Value != null);
}
