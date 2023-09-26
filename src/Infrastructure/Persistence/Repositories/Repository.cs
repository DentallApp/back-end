namespace DentallApp.Infrastructure.Persistence.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
    private readonly AppDbContext _context;

    protected AppDbContext Context => _context;

    public Repository(AppDbContext context)
    {
        _context = context;
    }

    public Task<TEntity> GetByIdAsync(int id)
    {
        return _context.Set<TEntity>()
            .Where(entity => entity.Id == id)
            .FirstOrDefaultAsync();
    }

    public void Add(TEntity entity)
    {
        _context.Add(entity);
    }

    public void Remove(TEntity entity)
    {
        _context.Remove(entity);
    }
}
