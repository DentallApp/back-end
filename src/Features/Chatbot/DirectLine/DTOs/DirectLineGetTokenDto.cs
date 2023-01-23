namespace DentallApp.Features.Chatbot.DirectLine.DTOs;

public class DirectLineGetTokenDto
{
    public string ConversationId { get; set; }
    public string Token { get; set; }
    [JsonProperty("expires_in")]
    public int ExpiresIn { get; set; }
}
