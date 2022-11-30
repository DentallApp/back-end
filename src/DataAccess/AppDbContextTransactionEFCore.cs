namespace DentallApp.DataAccess;

public class AppDbContextTransactionEFCore : IAppDbContextTransaction
{
    private readonly IDbContextTransaction _transaction;

    public AppDbContextTransactionEFCore(IDbContextTransaction transaction)
    {
        _transaction = transaction;
    }

    public void Commit()
        => _transaction.Commit();

    public async Task CommitAsync()
        => await _transaction.CommitAsync();

    public void Rollback()
        => _transaction.Rollback();

    public async Task RollbackAsync()
        => await _transaction.RollbackAsync();

    public void Dispose()
        => _transaction.Dispose();

    public async ValueTask DisposeAsync()
        => await _transaction.DisposeAsync();
}
