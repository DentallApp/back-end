namespace DentallApp.DataAccess.DbConnectors;

public interface IDbConnector
{
    IDbConnection CreateConnection();
}
