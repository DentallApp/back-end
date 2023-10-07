namespace DentallApp.Features.ChatBot.Helpers;

/// <summary>
/// <para>Prompts a user to enter information on the adaptive card.</para>
/// Reference: <seealso href="https://binarygrounds.com/2021/06/12/adaptive-cards-in-bot-dialog.html">
/// Use Adaptive Cards as dialog in Bot Framework.
/// </seealso>
/// </summary>
public class AdaptiveCardPrompt : Prompt<JObject>
{
    public AdaptiveCardPrompt(string dialogId, PromptValidator<JObject> validator = null)
        : base(dialogId, validator)
    {
    }

    protected override async Task OnPromptAsync(ITurnContext turnContext, IDictionary<string, object> state, PromptOptions options, bool isRetry, CancellationToken cancellationToken = default)
    {
        if (turnContext == null)
        {
            throw new ArgumentException(nameof(turnContext));
        }

        if (options == null)
        {
            throw new ArgumentException(nameof(options));
        }

        if (isRetry && options.Prompt != null)
        {
            await turnContext.SendActivityAsync(options.RetryPrompt, cancellationToken).ConfigureAwait(false);
        }
        else if (options.Prompt != null)
        {
            await turnContext.SendActivityAsync(options.Prompt, cancellationToken).ConfigureAwait(false);
        }
    }

    protected override Task<PromptRecognizerResult<JObject>> OnRecognizeAsync(ITurnContext turnContext, IDictionary<string, object> state, PromptOptions options, CancellationToken cancellationToken = default)
    {
        if (turnContext == null)
        {
            throw new ArgumentException(nameof(turnContext));
        }

        if (turnContext.Activity == null)
        {
            throw new ArgumentException(nameof(turnContext));
        }

        var result = new PromptRecognizerResult<JObject>();

        if (turnContext.Activity.Type == ActivityTypes.Message)
        {
            if (turnContext.Activity.Value != null)
            {
                if (turnContext.Activity.Value is JObject)
                {
                    result.Value = turnContext.Activity.Value as JObject;
                    result.Succeeded = true;
                }
            }

        }

        return Task.FromResult(result);
    }
}
