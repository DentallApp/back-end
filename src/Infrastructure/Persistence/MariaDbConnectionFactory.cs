namespace DentallApp.Infrastructure.Persistence;

public class MariaDbConnectionFactory(string connectionString) : IDbConnectionFactory
{
    public IDbConnection CreateConnection()
    {
        return new MySqlConnection(connectionString);
    }
}
