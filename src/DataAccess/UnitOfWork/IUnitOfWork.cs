namespace DentallApp.DataAccess.UnitOfWork;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync();
}