namespace DentallApp.Features.Chatbot.DirectLine.Services;

/// <summary>
/// Represents the Direct Line channel of Azure Bot.
/// </summary>
public class DirectLineAzureService : DirectLineService
{
    private readonly DirectLineSettings _directLineSettings;

    public DirectLineAzureService(IHttpClientFactory httpFactory, DirectLineSettings directLineSettings) 
        : base(httpFactory, namedClient: nameof(DirectLineAzureService))
    {
        _directLineSettings = directLineSettings;
    }

    /// <summary>
    /// Adds the prefix <c>dl_</c> to the user ID, as required by the Direct Line API.
    /// </summary>
    /// <param name="userId">The user ID to add the prefix.</param>
    /// <returns>The user ID with the prefix.</returns>
    private static string AddPrefixToUserId(int userId)
        => $"dl_{userId}";

    /// <inheritdoc />
    public async override Task<Response<DirectLineGetTokenDto>> GetTokenAsync(int userId)
    {
        var requestBody = new 
        { 
            user = new 
            { 
                id = AddPrefixToUserId(userId)
            } 
        };
        var tokenRequest = new HttpRequestMessage(HttpMethod.Post, RequestUri)
        {
            Headers =
            {
                { "Authorization", $"Bearer {_directLineSettings.DirectLineSecret}" },
            },
            Content = new StringContent(content: JsonConvert.SerializeObject(requestBody),
                                        Encoding.UTF8,
                                        MediaTypeNames.Application.Json),
        };

        var tokenResponseMessage = await Client.SendAsync(tokenRequest, default);

        if (!tokenResponseMessage.IsSuccessStatusCode)
            return new Response<DirectLineGetTokenDto> { Message = DirectLineTokenFailedMessage };

        var responseContentString = await tokenResponseMessage.Content.ReadAsStringAsync();
        var tokenResponse = JsonConvert.DeserializeObject<DirectLineGetTokenDto>(responseContentString);

        return new Response<DirectLineGetTokenDto>
        {
            Success = true,
            Data    = tokenResponse,
            Message = GetResourceMessage
        };
    }
}
