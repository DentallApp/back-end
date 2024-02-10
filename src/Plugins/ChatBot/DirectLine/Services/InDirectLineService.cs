namespace Plugin.ChatBot.DirectLine.Services;

/// <summary>
/// Represents an own implementation of Direct Line API that runs without Azure.
/// <para>See <see href="https://github.com/newbienewbie/InDirectLine" />.</para>
/// </summary>
public class InDirectLineService : DirectLineService
{
    public InDirectLineService(IHttpClientFactory httpFactory) 
        : base(httpFactory, namedClient: nameof(InDirectLineService)) { }

    /// <inheritdoc />
    public async override Task<Result<GetDirectLineTokenResponse>> GetTokenAsync(AuthenticatedUser user)
    {
        var requestBody = new
        {
            userId   = GenerateUserIdForDirectLine(user),
            password = ""
        };
        var tokenRequest = new HttpRequestMessage(HttpMethod.Post, RequestUri)
        {
            Content = new StringContent(
                content: JsonConvert.SerializeObject(requestBody),
                Encoding.UTF8,
                MediaTypeNames.Application.Json)
        };

        var tokenResponseMessage = await Client.SendAsync(tokenRequest, default);

        if (!tokenResponseMessage.IsSuccessStatusCode)
            return Result.CriticalError(Messages.DirectLineTokenFailed);

        var responseContentString = await tokenResponseMessage.Content.ReadAsStringAsync();
        var tokenResponse = JsonConvert.DeserializeObject<GetDirectLineTokenResponse>(responseContentString);
        return Result.ObtainedResource(tokenResponse);
    }
}
