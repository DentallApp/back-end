namespace Plugin.ChatBot.DirectLine;

public class DirectLineSettings
{
    public const string DirectLineSecretSetting  = "DIRECT_LINE_SECRET";
    public const string DirectLineBaseUrlSetting = "DIRECT_LINE_BASE_URL";
    public const string DefaultBaseUrl           = "https://directline.botframework.com/";
    public const string DefaultServiceName       = nameof(DirectLineAzureService);

    public string DirectLineSecret { get; set; }
    private string DirectLineBaseUrl { get; set; }

    public string GetDirectLineBaseUrl()
        => string.IsNullOrWhiteSpace(DirectLineBaseUrl) ?
                  DefaultBaseUrl :
                  DirectLineBaseUrl.TrimEnd('/') + "/";

    /// <summary>
    /// Gets the name of the Direct Line service based on the URL loaded from the .env file.
    /// </summary>
    /// <remarks>
    /// Available services:
    /// <list type="bullet">
    /// <item><see cref="InDirectLineService"/></item>
    /// <item><see cref="DirectLineAzureService"/></item>
    /// </list>
    /// </remarks>
    /// <returns>The name of the Direct Line service.</returns>
    public string GetServiceName()
    {
        var baseUrl = GetDirectLineBaseUrl();
        return string.IsNullOrWhiteSpace(baseUrl) ?
                  DefaultServiceName :
                  GetServiceName(baseUrl);
    }

    private string GetServiceName(string baseUrl)
        => baseUrl.StartsWith(DefaultBaseUrl) ?
                  DefaultServiceName :
                  nameof(InDirectLineService);
}
