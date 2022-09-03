namespace DentallApp.UnitOfWork;

public partial class UnitOfWorkEFCore : IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWorkEFCore(AppDbContext context)
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
