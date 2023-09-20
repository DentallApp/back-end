namespace DentallApp.Shared.Persistence;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync();
}