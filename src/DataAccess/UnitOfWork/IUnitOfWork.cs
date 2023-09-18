namespace DentallApp.DataAccess.UnitOfWork;

public interface IUnitOfWork
{
    IAppDbContextTransaction BeginTransaction();
    Task<IAppDbContextTransaction> BeginTransactionAsync();
    Task<int> SaveChangesAsync();
}