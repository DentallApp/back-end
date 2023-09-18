namespace DentallApp.DataAccess.UnitOfWork;

public partial class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public IAppDbContextTransaction BeginTransaction()
        => new AppDbContextTransactionEFCore(_context.Database.BeginTransaction());

    public async Task<IAppDbContextTransaction> BeginTransactionAsync()
        => new AppDbContextTransactionEFCore(await _context.Database.BeginTransactionAsync());

    public async Task<int> SaveChangesAsync()
        => await _context.SaveChangesAsync();
}
