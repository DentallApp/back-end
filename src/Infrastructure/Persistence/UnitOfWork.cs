namespace DentallApp.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly DbContext _context;

    public UnitOfWork(DbContext context)
    {
        _context = context;
    }

    public Task<int> SaveChangesAsync()
    {
        return _context.SaveChangesAsync();
    }
}
