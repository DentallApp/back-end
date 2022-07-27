namespace DentallApp.UnitOfWork;

public partial class UnitOfWorkEFCore : IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWorkEFCore(AppDbContext context)
    {
        _context = context;
    }

    public async Task<int> SaveChangesAsync()
        => await _context.SaveChangesAsync();

    public async Task RollbackAsync()
        => await _context.Database.RollbackTransactionAsync();
}
