namespace DentallApp.DataAccess.DbConnectors;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
}
