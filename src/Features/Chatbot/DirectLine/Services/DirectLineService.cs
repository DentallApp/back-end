﻿namespace DentallApp.Features.Chatbot.DirectLine.Services;

/// <summary>
/// Represents the client that obtains the Direct Line token.
/// </summary>
public abstract class DirectLineService
{
    public const string RequestUri = "v3/directline/tokens/generate";
    private readonly HttpClient _client;
    protected HttpClient Client => _client;

    /// <summary>
    /// Initializes a new instance of the <see cref="DirectLineService" /> class with a specified <see cref="HttpClient" />.
    /// </summary>
    /// <param name="client">An instance of <see cref="HttpClient" />.</param>
    public DirectLineService(HttpClient client)
        => _client = client;

    /// <summary>
    /// Initializes a new instance of the <see cref="DirectLineService" /> class with a specified factory and a reference with named client.
    /// </summary>
    /// <param name="httpFactory">A factory that can create <see cref="HttpClient" /> instances.</param>
    /// <param name="namedClient">The logical name of the client to create.</param>
    public DirectLineService(IHttpClientFactory httpFactory, string namedClient)
        => _client = httpFactory.CreateClient(namedClient);

    /// <summary>
    /// Gets the Direct Line token to access a single conversation associated with the bot.
    /// </summary>
    /// <param name="userId">The ID of the authenticated user.</param>
    /// <returns></returns>
    public abstract Task<Response<DirectLineGetTokenDto>> GetTokenAsync(int userId);
}