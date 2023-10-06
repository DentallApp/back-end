namespace DentallApp.Shared.Configuration;

/// <summary>
/// Represents the database configuration.
/// </summary>
public class DatabaseSettings
{
    public string DbConnectionString { get; set; }
    public int DbMaxRetryCount { get; set; }
    public int DbMaxRetryDelay { get; set;  }
}
