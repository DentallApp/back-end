namespace DentallApp.Shared.Interfaces.Persistence;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync();
}