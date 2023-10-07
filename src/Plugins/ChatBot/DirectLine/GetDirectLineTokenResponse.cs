namespace DentallApp.Features.ChatBot.DirectLine;

public class GetDirectLineTokenResponse
{
    public string ConversationId { get; init; }
    public string Token { get; init; }

    [JsonProperty("expires_in")]
    public int ExpiresIn { get; init; }
}
