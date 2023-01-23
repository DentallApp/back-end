namespace DentallApp.Configuration;

public class DirectLineSettings
{
    public const string DirectLineSecretSetting  = "DIRECT_LINE_SECRET";
    public const string DirectLineBaseUrlSetting = "DIRECT_LINE_BASE_URL";
    public const string DefaultBaseUrl           = "https://directline.botframework.com/"; 
    public const string DefaultProviderName      = nameof(DirectLineAzureService);

    public string DirectLineSecret { get; set; }
    private string DirectLineBaseUrl { get; set; }

    public string GetDirectLineBaseUrl()
        => string.IsNullOrWhiteSpace(DirectLineBaseUrl) ? 
                  DefaultBaseUrl : 
                  DirectLineBaseUrl.TrimEnd('/') + "/";

    public string GetProviderName()
    {
        var baseUrl = GetDirectLineBaseUrl();
        return string.IsNullOrWhiteSpace(baseUrl) ?
                  DefaultProviderName :
                  GetServiceName(baseUrl);
    }

    private static string GetServiceName(string baseUrl)
        => baseUrl.StartsWith(DefaultBaseUrl) ?
                  DefaultProviderName :
                  nameof(InDirectLineService);
}
