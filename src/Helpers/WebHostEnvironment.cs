namespace DentallApp.Helpers;

public static class WebHostEnvironment
{
    private const string AspNetCoreEnvironment = "ASPNETCORE_ENVIRONMENT";
    private const string Development = "Development";
    private const string Staging     = "Staging";
    private const string Production  = "Production";

    public static bool IsDevelopment() => IsEnvironment(Development);
    public static bool IsStaging()     => IsEnvironment(Staging);
    public static bool IsProduction()  => IsEnvironment(Production);

    public static bool IsEnvironment(string environmentName)
        => string.Equals(Environment.GetEnvironmentVariable(AspNetCoreEnvironment), 
                         environmentName, 
                         StringComparison.OrdinalIgnoreCase);
}
