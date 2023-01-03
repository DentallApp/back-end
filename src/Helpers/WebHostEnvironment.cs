namespace DentallApp.Helpers;

public static class WebHostEnvironment
{
    private const string _aspNetCoreEnvironment = "ASPNETCORE_ENVIRONMENT";
    private const string _development = "Development";
    private const string _staging     = "Staging";
    private const string _production  = "Production";

    public static bool IsDevelopment() => IsEnvironment(_development);
    public static bool IsStaging()     => IsEnvironment(_staging);
    public static bool IsProduction()  => IsEnvironment(_production);

    public static bool IsEnvironment(string environmentName)
        => string.Equals(Environment.GetEnvironmentVariable(_aspNetCoreEnvironment), 
                         environmentName, 
                         StringComparison.OrdinalIgnoreCase);
}
