namespace DentallApp.Features.Chatbot.DirectLine.Services;

/// <summary>
/// Represents an own implementation of Direct Line API that runs without Azure.
/// <para>See <see href="https://github.com/newbienewbie/InDirectLine" />.</para>
/// </summary>
public class InDirectLineService : DirectLineService
{
    public InDirectLineService(IHttpClientFactory httpFactory) 
        : base(httpFactory, namedClient: nameof(InDirectLineService)) { }

    /// <inheritdoc />
    public async override Task<Response<DirectLineGetTokenDto>> GetTokenAsync(UserProfile userProfile)
    {
        var requestBody = new
        {
            userId   = GenerateUserIdForDirectLine(userProfile),
            password = ""
        };
        var tokenRequest = new HttpRequestMessage(HttpMethod.Post, RequestUri)
        {
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
