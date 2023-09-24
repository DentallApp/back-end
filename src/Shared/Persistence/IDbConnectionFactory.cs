namespace DentallApp.Shared.Persistence;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
}
