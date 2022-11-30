using MySqlConnector;

namespace DentallApp.DataAccess.DbConnectors;

public class MariaDbConnector : IDbConnector
{
    private string ConnectionString { get; }

    public MariaDbConnector(string connectionString)
    {
        ConnectionString = connectionString;
    }

    public IDbConnection CreateConnection()
        => new MySqlConnection(ConnectionString);
}
