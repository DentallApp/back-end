namespace DentallApp.Shared.Interfaces.Persistence;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
}
