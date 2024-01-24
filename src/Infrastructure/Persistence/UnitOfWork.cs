namespace DentallApp.Infrastructure.Persistence;

public class UnitOfWork(DbContext context) : IUnitOfWork
{
    public Task<int> SaveChangesAsync()
    {
        return context.SaveChangesAsync();
    }
}
