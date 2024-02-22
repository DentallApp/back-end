namespace DentallApp.Infrastructure;

/// <summary>
/// Represents the database configuration.
/// </summary>
public class DatabaseSettings
{
    [EnvKey("DB_USERNAME")]
    public string DbUserName { get; set; } = string.Empty;
    public string DbPassword { get; set; } = string.Empty;
    public string DbHost { get; set; } = string.Empty;
    public string DbDatabase { get; set; } = string.Empty;
    public uint DbPort { get; set; }
    public int DbMaxRetryCount { get; set; }
    public int DbMaxRetryDelay { get; set;  }
}
