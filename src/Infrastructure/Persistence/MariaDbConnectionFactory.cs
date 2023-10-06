namespace DentallApp.Infrastructure.Persistence;

public class MariaDbConnectionFactory : IDbConnectionFactory
{
    private readonly string _connectionString;

    public MariaDbConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IDbConnection CreateConnection()
    {
        return new MySqlConnection(_connectionString);
    }
}
