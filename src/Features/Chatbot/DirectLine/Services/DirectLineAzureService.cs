﻿namespace DentallApp.Features.Chatbot.DirectLine.Services;

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

    /// <inheritdoc />
    public async override Task<Response<GetDirectLineTokenResponse>> GetTokenAsync(AuthenticatedUser user)
    {
        var requestBody = new 
        {
            user = new
            { 
                id = $"{Prefix}{GenerateUserIdForDirectLine(user)}"
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
            return new Response<GetDirectLineTokenResponse> { Message = DirectLineTokenFailedMessage };

        var responseContentString = await tokenResponseMessage.Content.ReadAsStringAsync();
        var tokenResponse = JsonConvert.DeserializeObject<GetDirectLineTokenResponse>(responseContentString);

        return new Response<GetDirectLineTokenResponse>
        {
            Success = true,
            Data    = tokenResponse,
            Message = GetResourceMessage
        };
    }
}
