namespace DentallApp.DataAccess.DbTransactions;

public interface IAppDbContextTransaction : IDisposable, IAsyncDisposable
{
    void Commit();
    Task CommitAsync();
    void Rollback();
    Task RollbackAsync();
}
