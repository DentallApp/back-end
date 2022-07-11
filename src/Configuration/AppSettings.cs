namespace DentallApp.Configuration;

public class AppSettings
{
    [EnvKey("CONNECTION_STRING")]
    public string ConnectionString { get; set; }
}