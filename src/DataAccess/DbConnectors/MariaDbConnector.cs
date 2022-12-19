using MySqlConnector;

namespace DentallApp.DataAccess.DbConnectors;

public class MariaDbConnector : IDbConnector
{
    private readonly string _connectionString;

    public MariaDbConnector(string connectionString)
        => _connectionString = connectionString;

    public IDbConnection CreateConnection()
        => new MySqlConnection(_connectionString);
}
